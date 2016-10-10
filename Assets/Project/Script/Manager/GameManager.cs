using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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
        Death,
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
