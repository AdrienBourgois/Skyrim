using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour {

    static private DataManager instance;
    static public DataManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject gao = GameObject.FindGameObjectWithTag("DataManager");
                if (gao)
                    instance = gao.GetComponent<DataManager>();
            }

            return instance;
        }
    }


    private GameManager gameMgr;


    public string GetActiveSceneName
    {
        get { return SceneManager.GetActiveScene().name; }
    }
    public int GetActiveSceneId
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }

    private void Awake()
    {
        instance = this;
        gameMgr = GameManager.Instance;
    }

    public void LoadLevelFromGameState()
    {
        if (!gameMgr)
            return;

        SceneManager.LoadSceneAsync((int)GameManager.GameState.MainMenu);
    }


}
