using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {

    Player player;

    RectTransform manaBar;
    Text manaPoint;

    void Start()
    {
        manaBar = transform.FindChild("ManaBar").GetComponent<RectTransform>();
        manaPoint = transform.FindChild("ManaPoint").GetComponent<Text>();

        GameObject player_gao = GameObject.FindGameObjectWithTag("Player");

        if (player_gao)
            player = player_gao.GetComponent<Player>();
    }


    void Update()
    {
        Characteristics player_stats = player.CharacterStats.UnitCharacteristics;
        float mana_ratio = (float)player_stats.Mana / (float)player_stats.MaxMana;

        //Debug.Log(mana_ratio);

        if (player_stats.Mana >= 0)
        {
            //manaBar.localScale = new Vector3(mana_ratio, manaBar.localScale.y, manaBar.localScale.z);
            manaPoint.text = player_stats.Mana.ToString();
        }
    }
}
