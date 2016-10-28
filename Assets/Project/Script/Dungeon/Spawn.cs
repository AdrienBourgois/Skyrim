using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    
	private void Start () {

        StartCoroutine(TeleportPlayer());
    }

    private IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(0.1f);
        Player player = FindObjectOfType<Player>();
        if (player != null)
            player.transform.position = transform.position;
    }
	
}
