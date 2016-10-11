using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

    [SerializeField] float lookDownMax = -70f;
    [SerializeField] float lookUpMax = 70f;
    [SerializeField] float sensibility = 1f;

    Transform target;

    private float rotY = 0f;

	void Start ()
    {
        Player player = FindObjectOfType<Player>();
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
            transform.position = target.position;

        float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;

        rotY += Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, lookDownMax, lookUpMax);

        target.transform.localEulerAngles = new Vector3(-rotY, rotX , 0f);
        transform.localEulerAngles = new Vector3(-rotY, rotX, 0f);
    }
}
