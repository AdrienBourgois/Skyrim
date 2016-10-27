using UnityEngine;
using System.Collections.Generic;

//Module managed by DungeonManager

public class Module : MonoBehaviour {

    [SerializeField]
    private string[] tags;

    private List<ModuleConnector> moduleConnectorList = new List<ModuleConnector>();
    private List<ItemsGenerator> itemsGeneratorList = new List<ItemsGenerator>();
    private List<EnemySpawner> enemySpawnersList = new List<EnemySpawner>();

    private List<ModuleConnector> ModuleConnectorList { get { return moduleConnectorList; }}

    public string[] Tags { get { return tags; } }

    public List<ItemsGenerator> ItemsGeneratorList { get { return itemsGeneratorList; } }

    private void Awake()
    {
        DungeonManager.Instance.RegisterModule(this);
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
