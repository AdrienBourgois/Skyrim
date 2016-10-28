using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{
    public void OnUse(ACharacter _character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.LoadGame);
    }


}
