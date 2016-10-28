using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{
    public void OnUse(ACharacter _character)
    {
       StartCoroutine(TeleportToTown(_character));
    }

    private IEnumerator TeleportToTown(ACharacter character)
    {
        SceneManager.LoadSceneAsync("BaseScene");
        yield return new WaitForSeconds(0.1f);
    }     
}
