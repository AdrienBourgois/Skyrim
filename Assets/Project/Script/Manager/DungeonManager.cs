using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour {

    List<Module> modules = new List<Module>();
    DungeonGenerator dungeonGenerator = null;

    static private DungeonManager instance;
    static public DungeonManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject gao = GameObject.FindGameObjectWithTag("DungeonManager");
                if (gao)
                    instance = gao.GetComponent<DungeonManager>();

                //Debug.Log(gao);
            }

            return instance;
        }
    }

    void Awake()
    {
        dungeonGenerator = GetComponent<DungeonGenerator>();
    }

    void Start () {

        GameManager.Instance.onStateChanged += OnStateChanged;
        StartCoroutine(CreateDungeon());
        CheckSlot();
    }

    void Update () {

	}


    public void RegisterModule(Module m)
    {
        modules.Add(m);
    }


    void CheckSlot()
    {
        foreach (Module m in modules)
            foreach (ModuleConnector slot in m.ModuleConnectorList)
                if (slot.IsConnected == false)
                {
                    print(slot.transform.position);
                }
    }

    void OnStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.EnterDungeon)
        {
            print("EnterDungeon");
            StartCoroutine(CreateDungeon());
        }
    }

    IEnumerator CreateDungeon()
    {
        yield return new WaitForSeconds(0.1f);
        dungeonGenerator.GenerateDungeon();
    }
}
