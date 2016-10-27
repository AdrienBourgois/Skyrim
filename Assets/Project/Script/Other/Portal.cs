using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IUsableObject
{

    public void OnUse(ACharacter character)
    {
        LoadLevel(character);
    }

    private void LoadLevel(ACharacter character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.EnterDungeon);
        //DontDestroyOnLoad(FindObjectOfType<DungeonManager>());
        DontDestroyOnLoad(character);
        DontDestroyOnLoad(FindObjectOfType<Cam>());
        //DontDestroyOnLoad(FindObjectOfType<GameManager>());
        //DontDestroyOnLoad(FindObjectOfType<IGGui>());
        SceneManager.LoadSceneAsync("DungeonGeneration");
    }
}
