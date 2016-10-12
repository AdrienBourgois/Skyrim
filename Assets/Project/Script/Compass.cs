using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {

    //Transform[] targets;
    Transform target;
    Transform player;
    Transform needle;

    GameObject arrow1;
    GameObject arrow2;

    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("CompassTarget").transform;
        GameObject player_gao = GameObject.FindGameObjectWithTag("Player");
        if (player_gao == null)
            Debug.LogError("Compass.Start() - could not find player GameObject");
        else
            player = player_gao.transform;

        Transform compassCylinder = transform.FindChild("CompassCylinder");

        needle = compassCylinder.FindChild("Needle");
        arrow1 = compassCylinder.FindChild("Arrow1").gameObject;
        arrow2 = compassCylinder.FindChild("Arrow2").gameObject;
    }


    void Update()
    {
        if (!CheckTarget())
            return;

        if (arrow1.GetComponent<SpriteRenderer>().enabled == false)
        {
            needle.position = player.position;
            needle.LookAt(target);

            arrow1.GetComponent<SpriteRenderer>().enabled = true;
            arrow2.GetComponent<SpriteRenderer>().enabled = true;
            needle.forward = new Vector3(needle.forward.x, 0f, needle.forward.z);
            arrow1.transform.position += needle.forward /1.9f;
            arrow2.transform.position -= needle.forward /1.9f;
        }
    }

    bool CheckTarget()
    {
        GameObject target_gao = GameObject.FindGameObjectWithTag("CompassTarget");
        if (target_gao == null)
            return false;

        target = target_gao.transform;
        return true;
    }

    void DisableArrow()
    {
        arrow1.GetComponent<SpriteRenderer>().enabled = false;
        arrow2.GetComponent<SpriteRenderer>().enabled = false;
    }
}
