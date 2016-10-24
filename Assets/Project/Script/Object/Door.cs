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
        if (hasBeenOpen == false)
        {
            anim.Play("OpenDoor");
            hasBeenOpen = true;
            LoadLevel();
            
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

    void LoadLevel()
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.EnterDungeon);
        DontDestroyOnLoad(FindObjectOfType<DungeonManager>());
        SceneManager.LoadSceneAsync("DungeonGeneration");
    }
}
