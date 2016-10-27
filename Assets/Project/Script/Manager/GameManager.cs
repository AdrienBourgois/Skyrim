using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void DelegateState(GameState state);
    public event DelegateState onStateChanged;

    private AsyncOperation asyncSceneLoading;

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
    [SerializeField] private GameObject dataMgrPrefab;
    [SerializeField] private GameObject levelMgrPrefab;
    [SerializeField] private GameObject itemMgrPrefab;
    [SerializeField] private GameObject dungeonMgrPrefab;
    [SerializeField] private GameObject magicMgrPrefab;
    [SerializeField] private GameObject resourceMgrPrefab;
    #endregion

    private DataManager dataMgr;

    public enum GameState
    {
        Intro = 0,
        MainMenu,
        LoadGame,
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
        private set { currGameState = value; }
    }

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

        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 1)
            DontDestroyOnLoad(gameObject);
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

            case GameState.LoadGame:
                LoadLevel();
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
        AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Menu);
    }

    private void LoadLevel()
    {
        asyncSceneLoading = SceneManager.LoadSceneAsync("BaseScene");
        StartCoroutine(WaitForLoad());

        AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Game);
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitUntil(CheckIfSceneChanged);
        if (LevelManager.Instance == null)
            Instantiate(levelMgrPrefab).GetComponent<LevelManager>();

        ChangeGameStateTo(GameState.InGame);
    }

    private void InGameInit()
    {
        if (CurrGameState == GameState.Pause)
            OnPause();
        CurrGameState = GameState.InGame;
    }

    private bool CheckIfSceneChanged()
    {
        return asyncSceneLoading.isDone;
    }

    private void EnterDungeonInit()
    {
        currGameState = GameState.EnterDungeon;

        Instantiate(dungeonMgrPrefab).GetComponent<DungeonManager>();
    }

    private void PopulateDungeonInit()
    {
        currGameState = GameState.PopulateDungeon;
    }

    private void PauseInit()
    {
        OnPause();

        CurrGameState = GameState.Pause;
    }

}
