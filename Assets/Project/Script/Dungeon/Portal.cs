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
       
        DontDestroyOnLoad(character);
     
        SceneManager.LoadSceneAsync("DungeonGeneration");
    }
}
