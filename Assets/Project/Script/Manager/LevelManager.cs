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

                //Debug.Log(gao);
            }

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        player = FindObjectOfType<Player>();
	}
	
	void Update () {
	
	}
}
