using UnityEngine;

public class LevelManager : MonoBehaviour {
    private Player player;
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

    private void Awake()
    {
        instance = this;
    }

    private void Start ()
    {
        player = FindObjectOfType<Player>();
	}

    private void Update () {
	
	}
}
