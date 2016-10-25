using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    static public readonly string c_weaponChildName = "Weapons";

    public delegate void DelegateState(GameState state);
    public event DelegateState onStateChanged;


  
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
    [SerializeField] GameObject dungeonMgrPrefab;
    [SerializeField] GameObject magicMgrPrefab;
    [SerializeField] GameObject resourceMgrPrefab;
    #endregion

    private DataManager dataMgr;
    private GUIManager guiMgr;
    private LevelManager levelMgr;
    private ItemManager itemMgr;
    private DungeonManager dungeonMgr;
    private MagicManager magicMgr;
    private ResourceManager resourceMgr;

    public enum GameState
    {
        Intro = 0,
        MainMenu,
        InGame,
        EnterDungeon,
        PopulateDungeon,
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
        get { return currGameState == GameState.EnterDungeon; }
    }


    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
            Destroy(gameObject);

        instance = this;
        dataMgr = DataManager.Instance ? DataManager.Instance : Instantiate(dataMgrPrefab).GetComponent<DataManager>();
        guiMgr = GUIManager.Instance ? GUIManager.Instance : Instantiate(guiMgrPrefab).GetComponent<GUIManager>();
        itemMgr = ItemManager.Instance ? ItemManager.Instance : Instantiate(itemMgrPrefab).GetComponent<ItemManager>();
       // dungeonMgr = DungeonManager.Instance ? DungeonManager.Instance : Instantiate(dungeonMgrPrefab).GetComponent<DungeonManager>();
        magicMgr = MagicManager.Instance ? MagicManager.Instance : Instantiate(magicMgrPrefab).GetComponent<MagicManager>();
        resourceMgr = ResourceManager.Instance ? ResourceManager.Instance : Instantiate(resourceMgrPrefab).GetComponent<ResourceManager>();

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

            case "DungeonGenerator":
                EnterDungeonInit();
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

            case GameState.EnterDungeon:
                EnterDungeonInit();
                break;

            case GameState.PopulateDungeon:
                PopulateDungeonInit();
                break;

            case GameState.Pause:
                PauseInit();
                break;

            default:
                break;
        }

        if (onStateChanged != null)
            onStateChanged.Invoke(currGameState);

        //if (loadLevel)
           // dataMgr.LoadLevelFromGameState();
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

    void EnterDungeonInit()
    {
        currGameState = GameState.EnterDungeon;

        dungeonMgr = DungeonManager.Instance ? DungeonManager.Instance : Instantiate(dungeonMgrPrefab).GetComponent<DungeonManager>();
    }

    void PopulateDungeonInit()
    {
        currGameState = GameState.PopulateDungeon;
    }

    void PauseInit()
    {
        loadLevel = currGameState == GameState.InGame ? false : true;
        currGameState = GameState.Pause;
    }

}
