using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    private void Awake()
    {
        GameObject mGo = transform.parent.gameObject;
        Module m = mGo.GetComponent<Module>();
        if (m != null)
            m.AddEnemySpawner(this);
    }

    private void Start () {
	
	}

    public void CreateEnemy()
    {
        int score = Random.Range(0, 100);

        if (score > 50)
        {
            GameObject enemyPrefab = ResourceManager.Instance.Load("Character/Enemy");
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
        else
            Destroy(gameObject);
    }
}
