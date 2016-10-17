using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    Player player;
    public Player Player
    {
        get
        {
            if (!player)
                player = FindObjectOfType<Player>();

            return player;
        }
    }

    static private LevelManager instance;
    static public LevelManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject gao = GameObject.FindGameObjectWithTag("LevelManager");
                if (gao)
                    instance = gao.GetComponent<LevelManager>();
            }

            return instance;
        }
    }

    void Start () {
	
	}
	
	void Update () {
	
	}
}
