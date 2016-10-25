using UnityEngine;

public class MainMenuGui : MonoBehaviour
{
    [SerializeField] private GameObject panel = null;
    [SerializeField] private MenuButton buttonNewGame = null;
    [SerializeField] private MenuButton buttonLoadGame = null;
    [SerializeField] private MenuButton buttonExitGame = null;

    void Start()
    {
        buttonNewGame.OnClick += DisplayConfirmationPanel;
        buttonLoadGame.OnClick += DisplayConfirmationPanel;
        buttonExitGame.OnClick += DisplayConfirmationPanel;
    }

    public void DisplayConfirmationPanel(MenuButton.MenuButtonId id)
    {

        panel.SetActive(true);
        panel.GetComponent<ConfirmationPanel>().SetButtons(id);
        panel.GetComponent<ConfirmationPanel>().SetText(id);
    }
}
