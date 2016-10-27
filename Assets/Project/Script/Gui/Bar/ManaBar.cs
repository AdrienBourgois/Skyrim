﻿using UnityEngine;

public class ManaBar : Bar {
    private void Update()
    {
        Characteristics player_stats = player.CharacterStats.UnitCharacteristics;
        float mana_ratio = player_stats.Mana / player_stats.MaxMana;

        if (player_stats.Mana >= 0)
        {
            bar.localScale = new Vector3(mana_ratio, bar.localScale.y, bar.localScale.z);
            point.text = player_stats.Mana.ToString() + " / " + player_stats.MaxMana.ToString("0"); ;
        }
    }
}
