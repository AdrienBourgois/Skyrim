using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToTheMenuButton : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    public void ReturnToTheMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
