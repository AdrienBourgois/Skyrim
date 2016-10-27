﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{
    public void OnUse(ACharacter _character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.LoadGame);//StartCoroutine(TeleportToTown(_character));
    }

    private IEnumerator TeleportToTown()
    {
        Destroy(FindObjectOfType<Cam>());
        SceneManager.LoadSceneAsync("BaseScene");
        yield return new WaitForSeconds(0.1f);
    }     
}
