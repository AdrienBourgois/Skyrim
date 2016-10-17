using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

    //Singleton
    static private GUIManager instance;
    static public GUIManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject gao = GameObject.FindGameObjectWithTag("GuiManager");
                if (gao)
                    instance = gao.GetComponent<GUIManager>();
            }
            return instance;
        }
    }


    #region SerializeField
    [SerializeField]
    GameObject mainMenuGuiPrefab;
    [SerializeField]
    GameObject inGameGuiPrefab;
    [SerializeField]
    GameObject pauseGuiPrefab;
    #endregion


    GameManager gameMgr;


    void Awake()
    {
        gameMgr = GameManager.Instance;
        instance = this;
    }

    public void UpdateGui()
    {
        if (!gameMgr)
            return;

    }

    void addGui(GameObject gui_gao)
    {
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).name == gui_gao.name)
                return;
        }
        ResetGui();
        Instantiate(gui_gao).transform.SetParent(this.transform, false);
    }


    void ResetGui()
    {
        int childNb = transform.childCount;
        for (int i = 0; i < childNb; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
