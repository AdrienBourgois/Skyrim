using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour {
    private List<Module> modules = new List<Module>();
    private DungeonGenerator dungeonGenerator;


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
            }

            return instance;
        }
    }

    private void Awake()
    {
        dungeonGenerator = GetComponent<DungeonGenerator>();
    }

    private void Start () {

        GameManager.Instance.OnStateChanged += OnStateChanged;
        StartCoroutine(CreateDungeon());
    }

    public void RegisterModule(Module _m)
    {
        modules.Add(_m);
    }

    private void ItemGenerator()
    {

        foreach (Module m in modules.ToArray())
        {
            foreach (ItemsGenerator itGen in m.ItemsGeneratorList.ToArray())
            {
                if (itGen.IsConnected == false)
                {
                    itGen.CreateRandItem();
                }
            }
        }
    }

    private void EnemyGeneration()
    {
        foreach (Module m in modules.ToArray())
            foreach (EnemySpawner enemySpawner in m.EnemySpawnersList.ToArray())
                enemySpawner.CreateEnemy();
    }

    private void OnStateChanged(GameManager.GameState state)
    {
        //print("stateChanged");
        //if (state == GameManager.GameState.EnterDungeon)
        //    StartCoroutine(CreateDungeon());
        //if (state == GameManager.GameState.PopulateDungeon)
        //   ItemGenerator();
        
    }

    private IEnumerator CreateDungeon()
    {
        yield return new WaitForSeconds(0.1f);
        dungeonGenerator.GenerateDungeon();
        yield return new WaitForSeconds(0.1f);
        ItemGenerator();
        EnemyGeneration();
    }
}
