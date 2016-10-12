using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {

    //Transform[] targets;
    Transform target;
    Transform player;


    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("CompassTarget").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Use this to create compass with multiple target

        //GameObject[] target_gao = GameObject.FindGameObjectsWithTag("CompassTarget");
        //targets = new Transform[target_gao.Length];

        //for (int i = 0; i < targets.Length; i++)
        //    targets[i] = target_gao[i].transform;
    }


    void Update()
    {
        //needle.position = player.position;
        //needle.forward = Vector3.forward;

    }
}
