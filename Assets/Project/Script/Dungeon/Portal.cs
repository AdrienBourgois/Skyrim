using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IUsableObject
{
    public void OnUse(ACharacter _character)
    {
        LoadLevel(_character);
    }

    private void LoadLevel(ACharacter _character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.EnterDungeon);
       
        DontDestroyOnLoad(_character);
     
        SceneManager.LoadSceneAsync("DungeonGeneration");

    }
}
