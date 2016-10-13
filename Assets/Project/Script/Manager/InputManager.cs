using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private InputManager instance;
    public InputManager Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<InputManager>();

            return instance;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ;//GUIManager.Instance. 

	}
}
