using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    }

    void Update () {

        
	}


    public void RegisterModule(Module m)
    {
        modules.Add(m);
    }


    void CheckConnector()
    {
        foreach (Module m in modules)
            foreach (ModuleConnector slot in m.ModuleConnectorList)
                if (slot.IsConnected == false)
                    print(slot.transform.position);
                
    }

    void ItemGenerator()
    {
        //Use two for loop instead two foreach loop to avoid an Unity error with enumeration

        for (int i = 0; i < modules.Count; i++)
        {
            Module m = modules[i];
            for (int it = 0; it < m.ItemsGeneratorList.Count; it++)
            {
                ItemsGenerator itGen = m.ItemsGeneratorList[it];
                if (itGen.IsConnected == false)
                {
                    itGen.CreateRandItem();
                }
            }
        }
    }

    void OnStateChanged(GameManager.GameState state)
    {
        //if (state == GameManager.GameState.EnterDungeon)
        //    StartCoroutine(CreateDungeon());
        if (state == GameManager.GameState.PopulateDungeon)
           ItemGenerator();
        
    }

    IEnumerator CreateDungeon()
    {
        yield return new WaitForSeconds(0.1f);
        dungeonGenerator.GenerateDungeon();
        yield return new WaitForSeconds(0.1f);
        ItemGenerator();
    }
}
