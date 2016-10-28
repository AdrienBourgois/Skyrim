using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{
    public void OnUse(ACharacter _character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.LoadGame);
    }


}
