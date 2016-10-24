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


    GameManager gameMgr;


    public string getActiveSceneName
    {
        get { return SceneManager.GetActiveScene().name; }
    }
    public int getActiveSceneID
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }

    void Awake()
    {
        instance = this;
        gameMgr = GameManager.Instance;
    }

    public void LoadLevelFromGameState()
    {
        if (!gameMgr)
            return;

        SceneManager.LoadSceneAsync((int)gameMgr.CurrGameState);
    }


}
