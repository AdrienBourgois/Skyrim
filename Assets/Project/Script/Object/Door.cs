using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{

    private Animation anim = null;
    private bool hasBeenOpen = false;


    public void OnUse(ACharacter character)
    {
       StartCoroutine(TeleportToTown(character));
        
    }

    private void Start () {

        anim = GetComponent<Animation>();
        Door spawn = GetComponentInChildren<Door>();
        GameObject player = GameObject.Find("Player");
        if (player != null)
            player.transform.position = spawn.transform.position;
    }

    IEnumerator TeleportToTown(ACharacter character)
    {
        Destroy(FindObjectOfType<Cam>());
        SceneManager.LoadSceneAsync("BaseScene");
        yield return new WaitForSeconds(0.1f);
        
    }

     
}
