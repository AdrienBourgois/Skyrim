﻿using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    static public readonly string c_weaponChildName = "Weapons";

    public event Action onStateChanged;

    private GameState _state;

  
    static private GameManager instance;
    static public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                    Debug.LogWarning("GameManager.Instance - failed to find object of type GameManager");
            }
            return instance;
        }
    }

    bool loadLevel = true;

    #region SerializeField

    [SerializeField] GameObject dataMgrPrefab;
    [SerializeField] GameObject guiMgrPrefab;
    [SerializeField] GameObject levelMgrPrefab;
    [SerializeField] GameObject itemMgrPrefab;

    #endregion

    private DataManager dataMgr;
    private GUIManager guiMgr;
    private LevelManager levelMgr;
    private ItemManager itemMgr;

    public enum GameState
    {
        Intro = 0,
        MainMenu,
        InGame,
        EnterDungeon,
        Pause,
        Death,
        StateNb
    }
    private GameState currGameState;
    public GameState CurrGameState
    {
        get { return currGameState; }
    }

    public bool isEnterDungeon
    {
        get { return _state == GameState.EnterDungeon; }
    }


    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
            Destroy(gameObject);

        instance = this;
        dataMgr = DataManager.Instance ? DataManager.Instance : Instantiate(dataMgrPrefab).GetComponent<DataManager>();
        guiMgr = GUIManager.Instance ? GUIManager.Instance : Instantiate(guiMgrPrefab).GetComponent<GUIManager>();
        itemMgr = ItemManager.Instance ? ItemManager.Instance : Instantiate(itemMgrPrefab).GetComponent<ItemManager>();

        UpdateGameState();

        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 1)
            DontDestroyOnLoad(gameObject);
	}
	
    void UpdateGameState()
    {   
        switch (dataMgr.getActiveSceneName)
        {
            case "Intro":
                IntroInit(); 
                break;

            case "MainMenu":
                MainMenuInit(); 
                break;

            case "BaseScene":
                InGameInit(); 
                break;

            default:
                break;
        }
    }

    public void ChangeGameStateTo(GameState nextGameState)
    {
        switch (nextGameState)
        {
            case GameState.Intro:
                IntroInit();
                break;

            case GameState.MainMenu:
                MainMenuInit();
                break;

            case GameState.InGame:
                InGameInit();
                break;

            case GameState.Pause:
                PauseInit();
                break;

            default:
                break;
        }

        if (loadLevel)
            dataMgr.LoadLevelFromGameState();
    }

    void IntroInit()
    {
        currGameState = GameState.Intro;
    }

    void MainMenuInit()
    {
        
        currGameState = GameState.MainMenu;
    }

    void InGameInit()
    {
        loadLevel = currGameState == GameState.Pause ? false : true;
        currGameState = GameState.InGame;


        levelMgr = LevelManager.Instance ? LevelManager.Instance : Instantiate(levelMgrPrefab).GetComponent<LevelManager>();
    }

    void PauseInit()
    {
        loadLevel = currGameState == GameState.InGame ? false : true;
        currGameState = GameState.Pause;
    }

}
