using UnityEngine;
using System.Collections;

public class IGGui : MonoBehaviour
{
    GameObject pausePanel;
    InventoryGUI invGui;

    void Awake()
    {
        invGui = InventoryGUI.Instance;
        pausePanel = transform.FindChild("PausePanel").gameObject;
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
        pausePanel.transform.FindChild("MagicPanel").gameObject.SetActive(false);
        invGui.Show = false;
    }
}
