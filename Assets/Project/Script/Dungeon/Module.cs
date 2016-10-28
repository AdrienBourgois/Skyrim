using UnityEngine;
using System.Collections.Generic;

//Module managed by DungeonManager

public class Module : MonoBehaviour {

    [SerializeField]
    private string[] tags;

    private List<ModuleConnector> moduleConnectorList = new List<ModuleConnector>();
    private List<ItemsGenerator> itemsGeneratorList = new List<ItemsGenerator>();
    public List<EnemySpawner> enemySpawnersList = new List<EnemySpawner>();

    private List<ModuleConnector> ModuleConnectorList { get { return moduleConnectorList; }}

    public string[] Tags { get { return tags; } }

    public List<ItemsGenerator> ItemsGeneratorList { get { return itemsGeneratorList; } }

    public List<EnemySpawner> EnemySpawnersList { get { return enemySpawnersList; } }


    private void Awake()
    {
        DungeonManager.Instance.RegisterModule(this);
    }

    public ModuleConnector[] GetExits()
    {
        return GetComponentsInChildren<ModuleConnector>();
    }

    public void AddConnector(ModuleConnector _moduleConnector)
    {
        ModuleConnectorList.Add(_moduleConnector);
    }

    public void AddGenerator(ItemsGenerator _itemGenerator)
    {
        ItemsGeneratorList.Add(_itemGenerator);
    }

    public void AddEnemySpawner(EnemySpawner _enemySpawn)
    {
        enemySpawnersList.Add(_enemySpawn);
    }

 
}
