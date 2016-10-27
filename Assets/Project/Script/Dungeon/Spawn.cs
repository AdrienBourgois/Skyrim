using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    
	void Start () {

        StartCoroutine(TeleportPlayer());
    }

    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        Player player = FindObjectOfType<Player>();
        print(player);
        if (player != null)
            player.transform.position = transform.position;
    }
	
}
