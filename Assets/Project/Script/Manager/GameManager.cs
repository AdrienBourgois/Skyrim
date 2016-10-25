using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    static public readonly string c_weaponChildName = "Weapons";

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
    [SerializeField] private GameObject dataMgrPrefab = null;
    [SerializeField] private GameObject levelMgrPrefab = null;
    [SerializeField] private GameObject itemMgrPrefab = null;
    [SerializeField] private GameObject magicMgrPrefab = null;
    [SerializeField] private GameObject resourceMgrPrefab = null;
    #endregion

    private DataManager dataMgr;
    private LevelManager levelMgr;
    private ItemManager itemMgr;
    private MagicManager magicMgr;
    private ResourceManager resourceMgr;

    public enum GameState
    {
        Intro = 0,
        MainMenu,
        InGame,
        Pause,
        Death,
        StateNb
    }

    public GameState CurrGameState { get; private set; }

    public delegate void Pause();
    public static event Pause OnPause;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
            Destroy(gameObject);

        instance = this;
        dataMgr = DataManager.Instance ? DataManager.Instance : Instantiate(dataMgrPrefab).GetComponent<DataManager>();
        itemMgr = ItemManager.Instance ? ItemManager.Instance : Instantiate(itemMgrPrefab).GetComponent<ItemManager>();
        magicMgr = MagicManager.Instance ? MagicManager.Instance : Instantiate(magicMgrPrefab).GetComponent<MagicManager>();
        resourceMgr = ResourceManager.Instance ? ResourceManager.Instance : Instantiate(resourceMgrPrefab).GetComponent<ResourceManager>();
        levelMgr = LevelManager.Instance ? LevelManager.Instance : Instantiate(levelMgrPrefab).GetComponent<LevelManager>();

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

            case GameState.Death:
                break;
            case GameState.StateNb:
                break;

            default:
                break;
        }

        if (loadLevel)
            dataMgr.LoadLevelFromGameState();
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

    private void PauseInit()
    {
        loadLevel = CurrGameState != GameState.InGame;
        CurrGameState = GameState.Pause;

        if (!loadLevel)
            OnPause();
    }

}
