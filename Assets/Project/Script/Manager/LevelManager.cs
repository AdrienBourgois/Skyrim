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
            }

            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void InstanceGame()
    {
        if (!player)
        {
            player = Instantiate(ResourceManager.Instance.Load<Player>("Character/Player"));
            
            if (player == null)
                Debug.LogError("LevelManager.Awake() - could not load Player from Prefab Character/Player.");
        }

        if (!FindObjectOfType<Cam>())
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera != null)
                Destroy(mainCamera);
            Instantiate(ResourceManager.Instance.Load<Cam>("Character/Main Camera"));
        }

        if (!FindObjectOfType<Compass>())
            Instantiate(ResourceManager.Instance.Load<Compass>("Gui/Compass"));

        if (!FindObjectOfType<IgGui>())
            Instantiate(ResourceManager.Instance.Load<IgGui>("Gui/inGameGui"));
    }
}
