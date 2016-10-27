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

                //Debug.Log(gao);
            }

            return instance;
        }
    }

    private void Awake()
    {
        dungeonGenerator = GetComponent<DungeonGenerator>();
    }

    private void Start () {

        GameManager.Instance.onStateChanged += OnStateChanged;
        StartCoroutine(CreateDungeon());
    }

    private void Update () {

        
	}


    public void RegisterModule(Module m)
    {
        modules.Add(m);
    }


    private void CheckConnector()
    {
        foreach (Module m in modules)
            foreach (ModuleConnector slot in m.ModuleConnectorList)
                if (slot.IsConnected == false)
                    print(slot.transform.position);
                
    }

    private void ItemGenerator()
    {
        //Use two for loop instead two foreach loop to avoid an Unity error with enumeration

        foreach (Module m in modules)
        {
            foreach (ItemsGenerator itGen in m.ItemsGeneratorList)
            {
                if (itGen.IsConnected == false)
                {
                    itGen.CreateRandItem();
                }
            }
        }
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
    }
}
