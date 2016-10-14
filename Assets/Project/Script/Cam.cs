using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

    [SerializeField] float lookDownMax = -70f;
    [SerializeField] float lookUpMax = 70f;
    [SerializeField] float sensibility = 1f;

    PlayerController player;
    Transform compass;

    private float rotY = 0f;

	void Start ()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null)
            Debug.LogError("Cam.Start() - could not find object of type Player");

        compass = GameObject.FindGameObjectWithTag("Compass").transform;

        transform.rotation = new Quaternion(player.transform.forward.x,
                                            player.transform.forward.y,
                                            player.transform.forward.z,
                                            0f);
	}
	

	void Update ()
    {
        transform.position = player.transform.position + (Vector3.up * 1.5f);

        float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;

        rotY += Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, lookDownMax, lookUpMax);

        player.ControllerLook(-rotY, rotX);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);

        rotX = compass.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility /2 * Time.deltaTime;
        GameObject.FindGameObjectWithTag("Compass").transform.localEulerAngles = new Vector3(0f, rotX, 0f);
    }
}
