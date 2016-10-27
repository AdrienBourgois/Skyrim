using UnityEngine;
using System.Collections.Generic;

//Module managed by DungeonManager

public class Module : MonoBehaviour {


    [SerializeField]
    private string[] tags;

    private List<ModuleConnector> moduleConnectorList = new List<ModuleConnector>();
    private List<ItemsGenerator> itemsGeneratorList = new List<ItemsGenerator>();
    private List<EnemySpawner> enemySpawnersList = new List<EnemySpawner>();

    public List<ModuleConnector> ModuleConnectorList
    {
        get
        {
            return moduleConnectorList;
        }

        set
        {
            moduleConnectorList = value;
        }
    }

    public string[] Tags
    {
        get
        {
            return tags;
        }

        set
        {
            tags = value;
        }
    }

    public List<ItemsGenerator> ItemsGeneratorList
    {
        get
        {
            return itemsGeneratorList;
        }

        set
        {
            itemsGeneratorList = value;
        }
    }

    public List<EnemySpawner> EnemySpawnersList
    {
        get
        {
            return enemySpawnersList;
        }

        set
        {
            enemySpawnersList = value;
        }
    }

    private void Awake()
    {
        DungeonManager.Instance.RegisterModule(this);
    }

    private void Start()
    {
    }


    public ModuleConnector[] GetExits()
    {
        return GetComponentsInChildren<ModuleConnector>();
    }

    public void AddConnector(ModuleConnector moduleConnector)
    {
        ModuleConnectorList.Add(moduleConnector);
    }

    public void AddGenerator(ItemsGenerator itemGenerator)
    {
        ItemsGeneratorList.Add(itemGenerator);
    }

    public void AddEnemySpawner(EnemySpawner enemySpawn)
    {
        enemySpawnersList.Add(enemySpawn);
    }
}
