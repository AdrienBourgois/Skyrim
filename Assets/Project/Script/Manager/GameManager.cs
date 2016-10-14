using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Singleton
    static private GameManager instance;
    static public GameManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            return instance;
        }
    }

    bool loadLevel = true;

    #region SerializeField

    [SerializeField] GameObject dataMgrPrefab;
    [SerializeField] GameObject guiMgrPrefab;
 
    #endregion

    private DataManager dataMgr;
    private GUIManager guiMgr;

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

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
            Destroy(gameObject);
    }

	void Start ()
    {
        if (!DataManager.Instance)
            dataMgr = Instantiate(dataMgrPrefab).GetComponent<DataManager>();
        if (!GUIManager.Instance)
            guiMgr = Instantiate(guiMgrPrefab).GetComponent<GUIManager>();

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
    }

    void PauseInit()
    {
        loadLevel = currGameState == GameState.InGame ? false : true;
        currGameState = GameState.Pause;
    }

}
