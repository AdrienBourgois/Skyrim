using UnityEngine;
using System.Collections.Generic;

//Module managed by DungeonManager

public class Module : MonoBehaviour {


    [SerializeField]
    private string[] tags;

    List<ModuleConnector> moduleConnectorList = new List<ModuleConnector>();
    List<ItemsGenerator> itemsGeneratorList = new List<ItemsGenerator>();

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

    void Awake()
    {
        DungeonManager.Instance.RegisterModule(this);
    }

    void Start()
    {
    }


    public ModuleConnector[] GetExits()
    {
        return GetComponentsInChildren<ModuleConnector>();
    }

 

    public void AddConnector(ModuleConnector mC)
    {
        ModuleConnectorList.Add(mC);
    }

    public void AddGenerator(ItemsGenerator itGen)
    {
        itemsGeneratorList.Add(itGen);
    }
}
