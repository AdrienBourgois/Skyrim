using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    private GUIManager instance;

    public GUIManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();

            return instance;
        }
    }

    [SerializeField]
    private GameObject panel;


    public delegate void Action();
    public event Action onNewGameButtonPressed;
    public event Action onLoadGameButtonPressed;
    public event Action onExitGameButtonPressed;


    void Start () {
        
	}
	
	void Update () {
	
	}

    public void DisplayConfirmationPanel()
    {
        panel.SetActive(true);
    }
}
