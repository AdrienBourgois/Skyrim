using UnityEngine;

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

    private bool loadLevel = true;

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

  

    public GameState CurrGameState { get; private set; }

    public delegate void Pause();
    public static event Pause OnPause;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
            Destroy(gameObject);

        instance = this;
        dataMgr = DataManager.Instance ? DataManager.Instance : Instantiate(dataMgrPrefab).GetComponent<DataManager>();
        Instantiate(itemMgrPrefab).GetComponent<ItemManager>();
        Instantiate(magicMgrPrefab).GetComponent<MagicManager>();
        Instantiate(resourceMgrPrefab).GetComponent<ResourceManager>();
        Instantiate(levelMgrPrefab).GetComponent<LevelManager>();

        RecoverGameState();

        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 1)
            DontDestroyOnLoad(gameObject);

       
	}

    private void RecoverGameState()
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

            case GameState.Death:
                break;
            case GameState.StateNb:
                break;

            default:
                break;
        }

        if (onStateChanged != null)
            onStateChanged.Invoke(currGameState);

    }

    private void IntroInit()
    {
        CurrGameState = GameState.Intro;
    }

    private void MainMenuInit()
    {
        CurrGameState = GameState.MainMenu;
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Menu);
    }

    private void InGameInit()
    {
        loadLevel = CurrGameState != GameState.Pause;
        CurrGameState = GameState.InGame;

        if (!loadLevel)
            OnPause();
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Game);
    }


    void EnterDungeonInit()
    {
        currGameState = GameState.EnterDungeon;

        Instantiate(dungeonMgrPrefab).GetComponent<DungeonManager>();
    }

    void PopulateDungeonInit()
    {
        currGameState = GameState.PopulateDungeon;
    }

    private void PauseInit()
    {
        loadLevel = CurrGameState != GameState.InGame;
        CurrGameState = GameState.Pause;

        if (!loadLevel)
            OnPause();
    }

}
