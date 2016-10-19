using UnityEngine;
using System.Collections;

public class Module : MonoBehaviour {

    private bool test = false;

    void Start()
    {

        //print(transform.name);
    }

    public string[] Tags;

    public ModuleConnector[] GetExits()
    {
        return GetComponentsInChildren<ModuleConnector>();
    }

    void OnTriggerEnter(Collider collider)
    {
        //if (collider.transform.parent != null)
        // {
        
        if (collider.transform.localPosition == transform.localPosition && test == false)
        {
            print("Collider : " + collider.name);
            test = true;
            //if (collider.transform.parent != null)
            //    Destroy(collider.transform.parent.gameObject);
            //else
            //    Destroy(collider.gameObject);
        }
       // }
    }

}
