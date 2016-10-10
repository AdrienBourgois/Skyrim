using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

    private DataManager instance;
    public DataManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();

            return instance;
        }
    }


    void Start () {
	
	}
	
	void Update () {
	
	}
}
