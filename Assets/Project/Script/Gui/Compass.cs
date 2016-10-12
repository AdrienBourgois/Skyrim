using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {

    //Transform[] targets;
    Transform target;

    Transform needle_transform;

    Slider slider;

	void Start ()
    {
        slider = transform.GetComponentInChildren<Slider>();

        target = GameObject.FindGameObjectWithTag("CompassTarget").transform;
        needle_transform = transform.FindChild("Needle");
        
        //Use this to create compass with multiple target

        //GameObject[] target_gao = GameObject.FindGameObjectsWithTag("CompassTarget");
        //targets = new Transform[target_gao.Length];

        //for (int i = 0; i < targets.Length; i++)
        //    targets[i] = target_gao[i].transform;
    }
	

	void Update ()
    {
        needle_transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

        needle_transform.LookAt(target);
        Debug.Log(needle_transform.forward);
        slider.value = needle_transform.forward.x;
    }
}
