using UnityEngine;

public class MainMenuGui : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private MenuButton buttonNewGame;
    [SerializeField] private MenuButton buttonLoadGame;
    [SerializeField] private MenuButton buttonExitGame;

    private void Start()
    {
        buttonNewGame.OnClick += DisplayConfirmationPanel;
        buttonLoadGame.OnClick += DisplayConfirmationPanel;
        buttonExitGame.OnClick += DisplayConfirmationPanel;
    }

    private void DisplayConfirmationPanel(MenuButton.MenuButtonId _id)
    {

        panel.SetActive(true);
        panel.GetComponent<ConfirmationPanel>().SetButtons(_id);
        panel.GetComponent<ConfirmationPanel>().SetText(_id);
    }
}
