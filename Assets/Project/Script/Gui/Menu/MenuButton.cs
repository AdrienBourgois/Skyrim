using UnityEngine;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour
{
    [SerializeField]
    private Text confirmPanelText;
    protected Text ConfirmText
    {
        get { return confirmPanelText; }
        set { confirmPanelText = value; }
    }

    public enum MenuButtonId
    {
        NewGame,
        LoadGame,
        ExitGame,
        COUNT
    }

    public delegate void ClickAction(MenuButtonId id);
    public event ClickAction OnClick;

    protected void OnButtonClick(MenuButtonId id)
    {
        if (OnClick != null)
            OnClick.Invoke(id);
    }

    public abstract void OnButtonClick();
}
