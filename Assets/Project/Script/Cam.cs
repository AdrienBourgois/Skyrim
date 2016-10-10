using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

    [SerializeField] float sensibility = 1f; 

	void Start () {
	
	}
	

	void Update ()
    {
        //transform.position = target.position;

        float rotY = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
        float rotX = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;

        transform.localEulerAngles =  new Vector3(transform.localEulerAngles.x - rotX , transform.localEulerAngles.y + rotY, 0f);
    }
}
