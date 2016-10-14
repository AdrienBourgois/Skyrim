using UnityEngine;
using System.Collections;

public class IGGui : MonoBehaviour
{
    GameObject pausePanel;

    void Start()
    {
        pausePanel = transform.FindChild("PausePanel").gameObject;
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.CurrGameState != GameManager.GameState.Pause)
                Pause();
            else
                ReturnInGame();
        }
    }

    void Pause()
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        pausePanel.SetActive(true);
    }

    void ReturnInGame()
    { 
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.InGame);
        pausePanel.SetActive(false);
        pausePanel.transform.FindChild("SkillPanel").gameObject.SetActive(false);
        pausePanel.transform.FindChild("InventoryPanel").gameObject.SetActive(false);
        pausePanel.transform.FindChild("MagicPanel").gameObject.SetActive(false);
    }
}
