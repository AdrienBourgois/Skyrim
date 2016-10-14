using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

    #region SerializeField
    [Header("FpsCamera")]
    [SerializeField] float lookDownMax = -70f;
    [SerializeField] float lookUpMax = 70f;
    [SerializeField] float sensibility = 1f;

    [Header("TpsCamera")]
    [SerializeField] float TpslookDownMax = -50f;
    [SerializeField] float TpslookUpMax = 50f;
    [SerializeField] float distance = 10f;
    //[SerializeField] float height = 10f;
    [SerializeField] float heightDamp = 3f;
    [SerializeField] float rotationDamp = 3f;
    #endregion


    Player player;
    Transform compass;

    private float rotY = 0f;

    bool fpsCam = true;

	void Start ()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
            Debug.LogError("Cam.Start() - could not find object of type Player");

        compass = GameObject.FindGameObjectWithTag("Compass").transform;

        transform.rotation = new Quaternion(player.transform.forward.x,
                                            player.transform.forward.y,
                                            player.transform.forward.z,
                                            0f);
	}
	

	void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.C))
            ChangeCameraMode();

        transform.position = player.transform.position + (Vector3.up * 1.5f);

        FpsCamUpdate();

        if (fpsCam)
            FpsCamUpdate();
        else
            TpsCamUpdate();
    }

    void ChangeCameraMode()
    {
        fpsCam = !fpsCam;
        if (!fpsCam)
            GetComponent<Camera>().cullingMask |= (1 << LayerMask.NameToLayer("Player"));
        else
            GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("Player")); 
    }

    void FpsCamUpdate()
    {
        
        float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;

        rotY += Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, lookDownMax, lookUpMax);

        player.ControllerLook(-rotY, rotX);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);

        rotX = compass.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility / 2 * Time.deltaTime;
        GameObject.FindGameObjectWithTag("Compass").transform.localEulerAngles = new Vector3(0f, rotX, 0f);
    }

    void TpsCamUpdate()
    {
        float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;

        rotY += Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, TpslookDownMax, TpslookUpMax);

        player.ControllerLook(-rotY, rotX);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);

        rotX = compass.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility / 2 * Time.deltaTime;
        GameObject.FindGameObjectWithTag("Compass").transform.localEulerAngles = new Vector3(0f, rotX, 0f);

        transform.position -= player.transform.forward * distance;
    }
}
