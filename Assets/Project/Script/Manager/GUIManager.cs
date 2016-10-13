using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    static private GUIManager instance;
    static public GUIManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();

            return instance;
        }
    }
}
