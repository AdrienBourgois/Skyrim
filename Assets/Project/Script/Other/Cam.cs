using UnityEngine;
using System.Collections;

public class Cam : APausableObject
{
    [SerializeField] private float lookDownMax = -70f;
    [SerializeField] private float lookUpMax = 70f;
    [SerializeField] private float sensibility = 1f;
    [SerializeField]
    private float ratioOverHips = 0.75f;

    private PlayerController playerController;
    private Transform playerAnchor;
    private Transform compass;

    private float rotY;

    private void Awake()
    {
        GameManager.OnPause += PutPause;

        StartCoroutine(FindPlayer());

        //playerController = FindObjectOfType<PlayerController>();
        //if (playerController == null)
        //    Debug.LogError("Cam.Awake() - could not find object of type PlayerController");

        //playerAnchor = playerController.transform.FindChild("Hips");
        //if (playerAnchor == null)
        //    Debug.LogError("Cam.Awake() - could not find child of name Hips in playerController");

        //compass = GameObject.FindGameObjectWithTag("Compass").transform;

        //transform.rotation = new Quaternion(playerController.transform.forward.x,
        //                                    playerController.transform.forward.y,
        //                                    playerController.transform.forward.z,
        //                                    0f);
	}

    

    private void Update()
    {
        if (playerAnchor != null)
        {
            transform.position = playerAnchor.position + (Vector3.up * ratioOverHips);

            if (paused)
                return;

            FpsCamUpdate();
        }
    }

    private void FpsCamUpdate()
    {
        float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility;

        rotY += Input.GetAxis("Mouse Y") * sensibility;
        rotY = Mathf.Clamp(rotY, lookDownMax, lookUpMax);

        playerController.ControllerLook(-rotY, rotX);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);
        if (compass != null)
        {
            rotX = compass.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility / 2;
            GameObject.FindGameObjectWithTag("Compass").transform.localEulerAngles = new Vector3(0f, rotX, 0f);
        }
    }


    IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.1f);
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
   
}
