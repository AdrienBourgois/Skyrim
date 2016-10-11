using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Player player;

    RectTransform healthBar;
    Text healthPoint;

	void Start ()
    {
        healthBar = transform.FindChild("HealthBar").GetComponent<RectTransform>();
        healthPoint = transform.FindChild("HealthPoint").GetComponent<Text>();

        GameObject player_gao = GameObject.FindGameObjectWithTag("Player");

        if (player_gao)
            player = player_gao.GetComponent<Player>();
    }


    void Update ()
    {
        Characteristics player_stats = player.CharacterStats.UnitCharacteristics;
        float life_ratio = (float)player_stats.Health / (float)player_stats.MaxHealth;

        if (player_stats.Health >= 0)
        {
            healthBar.localScale = new Vector3(life_ratio, healthBar.localScale.y, healthBar.localScale.z);
            healthPoint.text = player_stats.Health.ToString();
        }
   }
}
