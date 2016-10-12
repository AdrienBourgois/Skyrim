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

        needle = transform.FindChild("Needle");
        arrow1 = transform.FindChild("CompassCylinder").FindChild("Arrow1").gameObject;
        arrow2 = transform.FindChild("CompassCylinder").FindChild("Arrow2").gameObject;
    }


    void Update()
    {
        if (!CheckTarget())
        {
            DisableArrow();
            return;
        }

        if (arrow1.GetComponent<SpriteRenderer>().enabled == false)
        {

            needle.position = player.position;
            needle.LookAt(target);
            needle.forward = new Vector3(needle.forward.x, 0f, needle.forward.z);

            Vector3 player_fwd_cpy = new Vector3(player.forward.x, 0f, player.forward.z);

            float rotAngle = Vector3.Angle(player_fwd_cpy, needle.forward);

            Debug.Log(rotAngle);
            arrow1.GetComponent<SpriteRenderer>().enabled = true;
            arrow2.GetComponent<SpriteRenderer>().enabled = true;
            arrow1.transform.position = transform.position + player_fwd_cpy /2f;
            arrow1.transform.RotateAround(transform.position, Vector3.up, -rotAngle);
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
