using UnityEngine;

public class Compass : MonoBehaviour {

    //Transform[] targets;
    private Transform target;
    private Transform player;

    private Transform needle;

    private Transform arrowRotator;
    private Transform arrow1;
    private Transform arrow2;

    private void Start()
    {
        CheckTarget();
        FindPlayer();
        
        needle = transform.FindChild("Needle");

        arrowRotator = transform.FindChild("ArrowRotator");
        arrow1 = arrowRotator.FindChild("Arrow1");
        arrow2 = arrowRotator.FindChild("Arrow2");
    }

    private void Update()
    {
        if (!CheckTarget())
        {
            DisableArrow();
            return;
        }
        if (player != null)
            needle.position = player.position;
        needle.LookAt(target);

        UpdateArrow();

    }

    private void UpdateArrow()
    {
        if (arrow1.GetComponent<SpriteRenderer>().enabled == false)
            EnableArrow();

        needle.forward = new Vector3(needle.forward.x, 0f, needle.forward.z);
        Vector3 player_fwd_cpy = new Vector3(player.forward.x, 0f, player.forward.z);


        float rotAngle = Vector3.Angle(player_fwd_cpy, needle.forward);

        //When target is on the rightside of the player
        if (Vector3.Angle(player.right, needle.forward) > 90f)
            rotAngle *= -1;

        arrowRotator.transform.eulerAngles = new Vector3(0f, -(rotAngle / 2f), 0f);
    }

    private void FindPlayer()
    {
        Player player_tmp = FindObjectOfType<Player>();

        if (player_tmp == null)
            Debug.LogError("Compass.Start() - could not find player");
        else
            player = player_tmp.transform;
    }

    private bool CheckTarget()
    {
        GameObject target_gao = GameObject.FindGameObjectWithTag("CompassTarget");
        if (target_gao == null)
        {
            Debug.LogError("Compass.CheckTarget() - any valid target");
            return false;
        }

        target = target_gao.transform;
        return true;
    }

    private void EnableArrow()
    {
        Vector3 player_fwd_cpy = new Vector3(player.forward.x, 0f, player.forward.z);

        arrow1.GetComponent<SpriteRenderer>().enabled = true;
        arrow2.GetComponent<SpriteRenderer>().enabled = true;
        arrow1.transform.position = transform.FindChild("CompassCylinder").position + player_fwd_cpy / 2f;
        arrow2.transform.position = transform.FindChild("CompassCylinder").position - player_fwd_cpy / 2f;
        // Bug when player is looking up or down
    }

    private void DisableArrow()
    {
        arrow1.GetComponent<SpriteRenderer>().enabled = false;
        arrow2.GetComponent<SpriteRenderer>().enabled = false;
    }
}
