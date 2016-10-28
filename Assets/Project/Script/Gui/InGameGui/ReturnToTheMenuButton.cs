using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTheMenuButton : MonoBehaviour {

	private void Start () {
	
	}
	
	private void Update () {
	
	}

    public void ReturnToTheMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.MainMenu);
    }
}
