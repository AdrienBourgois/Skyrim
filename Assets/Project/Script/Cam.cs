using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

    [SerializeField] float YAngleMin = -70f;
    [SerializeField] float YAngleMax = 70f;

    [SerializeField] float sensibility = 1f;

    Transform target;

    private float currRotX = 0f;

	void Start ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            target = player.transform;

            transform.rotation = new Quaternion(target.forward.x, target.forward.y, target.forward.z, 0f);
        }
        else
            Debug.Log("There is no Player :(");
	}
	

	void Update ()
    {
        if (target)
            transform.position = target.position; //+ Vector3.up;

        float rotY = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
        float rotX = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;

        Debug.Log(currRotX);

        currRotX += rotX;
        currRotX = Mathf.Clamp(currRotX, YAngleMin, YAngleMax);

        if (YAngleMin < currRotX && currRotX < YAngleMax)
        {
            target.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - rotX, transform.localEulerAngles.y + rotY, 0f);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - rotX, transform.localEulerAngles.y + rotY, 0f);
        }
    }
}
