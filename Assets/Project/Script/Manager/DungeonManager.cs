using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour {

    //public delegate
    List<Module> modules = new List<Module>();


    static private DungeonManager instance;
    static public DungeonManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("DungeonManager").GetComponent<DungeonManager>();

            return instance;
        }
    }

    void Start () {

       // test();

	}
	
	void Update () {
	
	}


    public void RegisterModule(Module m)
    {
        modules.Add(m);
    }


    void test()
    {
        foreach (Module m in modules)
            foreach (ModuleConnector slot in m.ModuleConnectorList)
                if (slot.IsConnected == false)
                {
                    print(slot.transform.position);
                }
    }
}
