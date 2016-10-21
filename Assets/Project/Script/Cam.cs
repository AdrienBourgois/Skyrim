using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{
    [SerializeField] float lookDownMax = -70f;
    [SerializeField] float lookUpMax = 70f;
    [SerializeField] float sensibility = 1f;
    [SerializeField]
    private float ratioOverHips = 0.75f;

    PlayerController playerController;
    Transform playerAnchor;
    Transform compass;

    private float rotY = 0f;
    
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
            Debug.LogError("Cam.Awake() - could not find object of type PlayerController");

        playerAnchor = playerController.transform.FindChild("Hips");
        if (playerAnchor == null)
            Debug.LogError("Cam.Awake() - could not find child of name Hips in playerController");

        compass = GameObject.FindGameObjectWithTag("Compass").transform;

        transform.rotation = new Quaternion(playerController.transform.forward.x,
                                            playerController.transform.forward.y,
                                            playerController.transform.forward.z,
                                            0f);
	}
	
	void Update()
    {
        transform.position = playerAnchor.position + (Vector3.up * ratioOverHips);

        float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;

        rotY += Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, lookDownMax, lookUpMax);

        playerController.ControllerLook(-rotY, rotX);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);

        rotX = compass.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility /2 * Time.deltaTime;
        GameObject.FindGameObjectWithTag("Compass").transform.localEulerAngles = new Vector3(0f, rotX, 0f);
    }
}
