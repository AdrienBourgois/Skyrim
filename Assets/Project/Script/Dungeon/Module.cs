using UnityEngine;
using System.Collections;

public class Module : MonoBehaviour {

    public string[] Tags;

    public ModuleConnector[] GetExits()
    {
        return GetComponentsInChildren<ModuleConnector>();
    }

    void OnTriggerEnter(Collider collider)
    {
        print("Collider : " + collider.name);
    }

}
