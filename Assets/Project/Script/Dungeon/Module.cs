using UnityEngine;
using System.Collections;

public class Module : MonoBehaviour {

    private bool test;

    void Start()
    {
        test = false;
        //print(transform.name);
    }

    public string[] Tags;

    public ModuleConnector[] GetExits()
    {
        return GetComponentsInChildren<ModuleConnector>();
    }

    void OnTriggerEnter(Collider collider)
    {
        
        
        
        
       // if (collider.transform.localPosition == transform.localPosition)
        //{
            print("Collider : " + collider.name);
           // collider.gameObject.transform.position = new Vector3(70, 2, 10);
           
           // Destroy(collider.gameObject);
            //if (collider.transform.parent != null)
            //    Destroy(collider.transform.parent.gameObject);
            //else
            //    Destroy(collider.gameObject);
       // }
       
    }

}
