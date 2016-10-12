using UnityEngine;
using UnityEngine.UI;

public class XPBar : Bar
{
    void Update()
    {
        Characteristics player_stats = player.CharacterStats.UnitCharacteristics;
        //float xp_ratio = (float)player_stats.Xp / (float)player_stats.XpToLevelUp;

        //if (player_stats.Xp <= player_stats.XpToLevelUp)
        //{
        //    bar.localScale = new Vector3(xp_ratio, bar.localScale.y, bar.localScale.z);
        //    point.text = player_stats.Xp.ToString() + " / " + player_stats.XpToLevelUp.ToString();
        //}
    }
}
