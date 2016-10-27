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
        if (player == null && GameManager.Instance.CurrGameState == GameManager.GameState.InGame)
            CreatePlayer();
    }

    private void Start ()
    {
        player = FindObjectOfType<Player>();
        

    }

    private void Update () {

        //if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            //Destroy(Player);

	}

    void CreatePlayer()
    {
        GameObject playerPrefab = ResourceManager.Instance.Load("Character/Player");
        GameObject player = Instantiate(playerPrefab);
    }
}
