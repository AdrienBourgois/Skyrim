using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public event Action onStateChanged;

    private GameState _state;

    private GameManager instance;
    public GameManager Instance
    {
        get
        {
            if (!instance)
                instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            return instance;
        }
    }

    enum GameState
    {
        Intro = 0,
        MainMenu,
        InGame,
        EnterDungeon,
        Pause,
        Death,
        StateNb
    }

    public bool isEnterDungeon
    {
        get { return _state == GameState.EnterDungeon; }
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
