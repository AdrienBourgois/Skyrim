using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void DelegateState(GameState _state);
    public event DelegateState OnStateChanged;

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

    #region SerializeField
    [SerializeField] private GameObject levelMgrPrefab;
    [SerializeField] private GameObject itemMgrPrefab;
    [SerializeField] private GameObject dungeonMgrPrefab;
    [SerializeField] private GameObject magicMgrPrefab;
    [SerializeField] private GameObject resourceMgrPrefab;
    [SerializeField] private GameObject audioMgrPrefab;
    #endregion

    public enum GameState
    {
        MainMenu = 0,
        LoadGame,
        InGame,
        EnterDungeon,
        PopulateDungeon,
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
        if (!ItemManager.Instance)
            Instantiate(itemMgrPrefab);
        if (!MagicManager.Instance)
            Instantiate(magicMgrPrefab);
        if (!ResourceManager.Instance)
            Instantiate(resourceMgrPrefab);
        if (!AudioManager.Instance)
            Instantiate(audioMgrPrefab);

        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 1)
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeGameStateTo(GameState.MainMenu);
    }

    public void ChangeGameStateTo(GameState _nextGameState)
    {
        switch (_nextGameState)
        {
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
                GameOverInit();
                break;
        }

        if (OnStateChanged != null)
            OnStateChanged.Invoke(CurrGameState);

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
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitUntil(CheckIfSceneChanged);
        if (LevelManager.Instance == null)
            Instantiate(levelMgrPrefab).GetComponent<LevelManager>();

        LevelManager.Instance.InstanceGame();
        AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Game);

        ChangeGameStateTo(GameState.InGame);
    }

    private void InGameInit()
    {
        if (CurrGameState == GameState.Pause)
            OnPause();
        CurrGameState = GameState.InGame;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private bool CheckIfSceneChanged()
    {
        return asyncSceneLoading.isDone;
    }

    private void EnterDungeonInit()
    {
        CurrGameState = GameState.EnterDungeon;

        Instantiate(dungeonMgrPrefab).GetComponent<DungeonManager>();
    }

    private void PopulateDungeonInit()
    {
        CurrGameState = GameState.PopulateDungeon;
    }

    public void PauseInit()
    {
        OnPause();

        CurrGameState = GameState.Pause;
    }

    private void GameOverInit()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadSceneAsync("GameOver");
    }
}
