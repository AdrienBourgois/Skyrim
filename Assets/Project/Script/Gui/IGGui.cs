using UnityEngine;

public class IGGui : MonoBehaviour
{
    private GameObject pausePanel;
    private InventoryGUI invGui;

    private void Awake()
    {
        invGui = InventoryGUI.Instance;
        pausePanel = transform.FindChild("PausePanel").gameObject;
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.Instance.CurrGameState != GameManager.GameState.Pause)
                Pause();
            else
                ReturnInGame();
        }
    }

    private void Pause()
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        pausePanel.SetActive(true);
    }

    private void ReturnInGame()
    { 
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.InGame);
        pausePanel.SetActive(false);
        pausePanel.transform.FindChild("SkillPanel").gameObject.SetActive(false);
        pausePanel.transform.FindChild("MagicPanel").gameObject.SetActive(false);
        if (invGui)
            invGui.Show = false;
    }
}
