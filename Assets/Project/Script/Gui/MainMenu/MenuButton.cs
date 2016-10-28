using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour
{
    [SerializeField]
    private Text confirmPanelText;
    [Useless]
    protected Text ConfirmText
    {
        get { return confirmPanelText; }
        set { confirmPanelText = value; }
    }

    public enum MenuButtonId
    {
        NewGame,
        LoadGame,
        ExitGame
    }

    public delegate void ClickAction(MenuButtonId _id);
    public event ClickAction OnClick;

    protected void OnButtonClick(MenuButtonId _id)
    {
        if (OnClick != null)
            OnClick.Invoke(_id);
    }

    [Useless]
    public abstract void OnButtonClick();
}
