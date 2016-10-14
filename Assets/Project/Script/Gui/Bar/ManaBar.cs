using UnityEngine;
using UnityEngine.UI;

public class ManaBar : Bar {

    void Update()
    {
        Characteristics player_stats = player.CharacterStats.UnitCharacteristics;
        float mana_ratio = (float)player_stats.Mana / (float)player_stats.MaxMana;

        if (player_stats.Mana >= 0)
        {
            bar.localScale = new Vector3(mana_ratio, bar.localScale.y, bar.localScale.z);
            point.text = player_stats.Mana.ToString();
        }
    }
}
