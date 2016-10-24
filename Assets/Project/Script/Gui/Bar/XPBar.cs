﻿using UnityEngine;
using UnityEngine.UI;

public class XPBar : Bar
{
    void Update()
    {
        float xp_ratio = (float)player.Xp / (float)player.XpToLevelUp;

        if (player.Xp <= player.XpToLevelUp)
        {
            bar.localScale = new Vector3(xp_ratio, bar.localScale.y, bar.localScale.z);
            point.text = player.Xp.ToString() + " / " + player.XpToLevelUp.ToString();
        }
    }
}
