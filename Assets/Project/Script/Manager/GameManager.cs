using UnityEngine;
using System.Collections;

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

    bool loadLevel = true;

    #region SerializeField
    [SerializeField] GameObject dataMgrPrefab = null;
    [SerializeField] GameObject levelMgrPrefab = null;
    [SerializeField] GameObject itemMgrPrefab = null;
    [SerializeField] GameObject magicMgrPrefab = null;
    [SerializeField] GameObject resourceMgrPrefab = null;
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
    private GameState currGameState;
    public GameState CurrGameState
    {
        get { return currGameState; }
    }

    public delegate void Pause();
    public static event Pause OnPause;

    void Awake()
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
	
    void RecoverGameState()
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
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Menu);
    }

    void InGameInit()
    {
        loadLevel = currGameState == GameState.Pause ? false : true;
        currGameState = GameState.InGame;

        if (!loadLevel)
            OnPause();
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Game);
    }

    void PauseInit()
    {
        loadLevel = currGameState == GameState.InGame ? false : true;
        currGameState = GameState.Pause;

        if (!loadLevel)
            OnPause();
    }

}
