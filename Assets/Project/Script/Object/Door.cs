using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{
    public void OnUse(ACharacter _character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.LoadGame);//StartCoroutine(TeleportToTown(_character));
    }

    [Useless]
    private IEnumerator TeleportToTown()
    {
        SceneManager.LoadSceneAsync("BaseScene");
        yield return new WaitForSeconds(0.1f);
    }     
}
