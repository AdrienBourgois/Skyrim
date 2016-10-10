using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    private LevelManager instance;
    public LevelManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

            return instance;
        }
    }

    void Start () {
	
	}
	
	void Update () {
	
	}
}
