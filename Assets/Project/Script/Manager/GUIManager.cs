using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    static private GUIManager instance;
    static public GUIManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();

            return instance;
        }
    }

    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private MenuButton buttonNewGame;
    [SerializeField]
    private MenuButton buttonLoadGame;
    [SerializeField]
    private MenuButton buttonExitGame;

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
