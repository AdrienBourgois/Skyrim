using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IUsableObject
{
    private Quaternion characterRotation;

    public void OnUse(ACharacter _character)
    {
        LoadLevel(_character);
    }

    private void LoadLevel(ACharacter _character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.EnterDungeon);
       
        DontDestroyOnLoad(_character);
     
        SceneManager.LoadSceneAsync("DungeonGeneration");

        characterRotation = _character.transform.rotation;
        characterRotation.y = 180f;
        _character.transform.rotation = characterRotation;

    }
}
