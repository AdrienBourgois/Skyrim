using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {

    //Transform[] targets;
    Transform target;
    Transform player;
    Transform needle;

    Transform arrowRotator;
    Transform arrow1;
    Transform arrow2;

    void Start()
    {
        CheckTarget();

        GameObject player_gao = GameObject.FindGameObjectWithTag("Player");
        if (player_gao == null)
            Debug.LogError("Compass.Start() - could not find player GameObject");
        else
            player = player_gao.transform;

        needle = transform.FindChild("Needle");

        arrowRotator = transform.FindChild("ArrowRotator");
        arrow1 = arrowRotator.FindChild("Arrow1");
        arrow2 = arrowRotator.FindChild("Arrow2");
    }


    void Update()
    {
        if (!CheckTarget())
        {
            DisableArrow();
            return;
        }

        needle.position = player.position;
        needle.LookAt(target);

        needle.forward = new Vector3(needle.forward.x, 0f, needle.forward.z);
        Vector3 player_fwd_cpy = new Vector3(player.forward.x, 0f, player.forward.z);

        float rotAngle = Vector3.Angle(player_fwd_cpy, needle.forward);

        if (Vector3.Angle(player.right, needle.forward) > 90f)
            rotAngle *= -1;


        if (arrow1.GetComponent<SpriteRenderer>().enabled == false)
        {
            arrow1.GetComponent<SpriteRenderer>().enabled = true;
            arrow2.GetComponent<SpriteRenderer>().enabled = true;
            arrow1.transform.position = transform.FindChild("CompassCylinder").position + player_fwd_cpy / 1.9f;
            arrow2.transform.position = transform.FindChild("CompassCylinder").position - player_fwd_cpy / 1.9f;
            // Bug when player is looking up or down
        }

        arrowRotator.transform.eulerAngles = new Vector3(0f, -(rotAngle /2f), 0f);
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
