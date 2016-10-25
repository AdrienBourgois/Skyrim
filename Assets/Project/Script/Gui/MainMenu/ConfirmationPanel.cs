﻿using UnityEngine;
using System;
using UnityEngine.UI;

public class ConfirmationPanel : MonoBehaviour
{
    [SerializeField]
    private Text panelText = null;

    private delegate void DelegateClick();
    private event DelegateClick OnYes = () => { };
    private event DelegateClick OnNo = () => { };

    public void DoYes()
    {
        OnYes.Invoke();
    }

    public void DoNo()
    {
        OnNo.Invoke();
    }

    public void SetText(MenuButton.MenuButtonId id)
    {
        SetPanelConfirmText(id);
    }

    private void SetPanelConfirmText(MenuButton.MenuButtonId id)
    {
        switch (id)
        {
            case MenuButton.MenuButtonId.NewGame:
                panelText.text = "Launch New Game ?";
                break;
            case MenuButton.MenuButtonId.LoadGame:
                panelText.text = "Load last saved game ?";
                break;
            case MenuButton.MenuButtonId.ExitGame:
                panelText.text = "Exit to desktop ?";
                break;
            default:
                panelText.text = "";
                break;
        }
    }

    public void SetButtons(MenuButton.MenuButtonId id)
    {
        SetYesButton(id);
        SetNoButton(id);
    }

    private void SetYesButton(MenuButton.MenuButtonId id)
    {
        switch (id)
        {
            case MenuButton.MenuButtonId.NewGame:
                OnYes = StartNewGame;
                break;
            case MenuButton.MenuButtonId.LoadGame:
                OnYes = LoadSaveGame;
                break;
            case MenuButton.MenuButtonId.ExitGame:
                OnYes = ExitGame;
                break;
            default:
                OnYes = () => { };
                break;
        }
    }

    private void StartNewGame()
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.InGame);
    }

    private void LoadSaveGame()
    {
        throw new NotImplementedException(); // Load the saved game
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetNoButton(MenuButton.MenuButtonId id)
    {
        OnNo = ClosePanel;
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}

