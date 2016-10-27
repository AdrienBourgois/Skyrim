using UnityEngine;

public class IGGui : MonoBehaviour
{
    private GameObject pausePanel;
    private GameObject gameMenuPanel;
    private InventoryPanelGui invPanelGui;

    private void Awake()
    {
        invGui = InventoryPanelGui.Instance;
        pausePanel = transform.FindChild("PausePanel").gameObject;
        pausePanel.SetActive(false);
        //gameMenuPanel = transform.FindChild("GameMenuPanel").gameObject;
        //gameMenuPanel.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (GameManager.Instance.CurrGameState != GameManager.GameState.Pause)
                //Pause(gameMenuPanel);
            //else
                ReturnInGame();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.Instance.CurrGameState != GameManager.GameState.Pause)
                Pause(pausePanel);
            else
                ReturnInGame();
        }
    }

    private void Pause(GameObject panelToShow)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        panelToShow.SetActive(true);
    }

    private void ReturnInGame()
    { 
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.InGame);
        //gameMenuPanel.SetActive(false);
        pausePanel.SetActive(false);
        pausePanel.transform.FindChild("SkillPanel").gameObject.SetActive(false);
        pausePanel.transform.FindChild("MagicPanel").gameObject.SetActive(false);
        if (invPanelGui)
            invPanelGui.Show = false;
    }
}
