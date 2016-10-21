using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    static public readonly string c_weaponChildName = "Weapons";

    static private GameManager instance;
    static public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                    Debug.LogWarning("GameManager.Instance - failed to find object of type GameManager");
            }
            return instance;
        }
    }

    enum GameState
    {
        Intro = 0,
        MainMenu,
        InGame,
        Pause,
        Death,
        StateNb
    }

	void Start ()
    {
        if (!Debug.isDebugBuild)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (GameObject.FindGameObjectsWithTag("GameManager").Length == 1)
            DontDestroyOnLoad(this);
	}
	

}
