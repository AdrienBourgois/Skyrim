using UnityEngine;
using System.Collections.Generic;

//Module managed by DungeonManager

public class Module : MonoBehaviour {


    [SerializeField]
    private string[] tags;

    private List<ModuleConnector> moduleConnectorList = new List<ModuleConnector>();
    private List<ItemsGenerator> itemsGeneratorList = new List<ItemsGenerator>();

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

 

    public void AddConnector(ModuleConnector _mC)
    {
        ModuleConnectorList.Add(_mC);
    }

    public void AddGenerator(ItemsGenerator _itGen)
    {
        itemsGeneratorList.Add(_itGen);
    }
}
