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
}
