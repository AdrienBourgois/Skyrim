using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{

    private Animation anim = null;
    private bool hasBeenOpen = false;


    public void OnUse(ACharacter character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.EnterDungeon);
        if (hasBeenOpen == false)
        {
            anim.Play("OpenDoor");
            hasBeenOpen = true;
            
            StartCoroutine(Test());
            
        }
        else if (hasBeenOpen == true)
        {
            anim.Play("CloseDoor");
            hasBeenOpen = false;
        }
    }

    void Start () {

        anim = GetComponent<Animation>();
    }
	
	void Update () {
	
	}

    void TeleportPlayerIntoTheDungeon(ACharacter player)
    {
        player.transform.position = transform.FindChild("SpawnPoint").transform.position;
    }

    IEnumerator Test()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("DungeonGeneration");
        yield return async;
        Debug.Log("Complete");
        //yield return new WaitForSeconds(1f);
        //SceneManager.LoadSceneAsync("DungeonGeneration");
    }
}
