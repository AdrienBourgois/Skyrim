using UnityEngine;

public class ItemsGenerator : MonoBehaviour {

    #region SerializeField
    //[SerializeField] private int chestProbability = 99;
    //[SerializeField] private int boxProbability = 20;
    //[SerializeField] private int barrelProbability = 20;
    //[SerializeField] private int torchProbability = 50;

    #endregion

    public bool IsConnected { get; set; }

    private void Awake()
    {
        GameObject mGo = transform.parent.gameObject;
        Module m = mGo.GetComponent<Module>();
        if (m != null)
            m.AddGenerator(this);
    }

    public void CreateRandItem()
    {
        int score = Random.Range(0, 100);

        if (score >= 90)
        {
            Object chestPrefab = ResourceManager.Instance.Load("Dungeon/chest_epic");
            GameObject chest = Instantiate(chestPrefab, transform.position, transform.rotation) as GameObject;
            chest.transform.SetParent(transform);
        }
        else if (score >= 60)
        {
            Object torchPrefab = ResourceManager.Instance.Load("Dungeon/Torch");
            GameObject torch = (GameObject)Instantiate(torchPrefab, transform.position, transform.rotation);
            torch.transform.SetParent(transform);
        }
        else 
        {
            int propScore = Random.Range(0, 100);

            if (propScore < 50)
            {
                Object barrelPrefab = ResourceManager.Instance.Load("Dungeon/Barrel");
                GameObject barrel = (GameObject)Instantiate(barrelPrefab, transform.position, transform.rotation);
                barrel.transform.SetParent(transform);
            }
            else
            {
                Object boxPrefab = ResourceManager.Instance.Load("Dungeon/Box");
                GameObject box = (GameObject)Instantiate(boxPrefab, transform.position, transform.rotation);
                box.transform.SetParent(transform);
            }
        }   
    }
}
