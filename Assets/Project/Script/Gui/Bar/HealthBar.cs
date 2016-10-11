using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    CharacterStats player_stats;

    RectTransform healthBar;

	void Start ()
    {
        healthBar = transform.FindChild("HealthBar").GetComponent<RectTransform>();

        GameObject player_gao = GameObject.FindGameObjectWithTag("Player");
        if (GameObject.FindGameObjectWithTag("Player"))
            player_stats = player_gao.GetComponent<Player>().Stats;//.UnitCharacteristics;
	}
	

	void Update ()
    {
        Debug.Log(player_stats);
        //float life_ratio = player_stats.Health / player_stats.MaxHealth;

        //healthBar.localScale = new Vector3(life_ratio, healthBar.localScale.y, healthBar.localScale.z);

	}
}
