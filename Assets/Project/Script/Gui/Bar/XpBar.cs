using UnityEngine;

public class XpBar : Bar
{
    private void Update()
    {
        float xpRatio = player.Xp / player.XpToLevelUp;

        if (player.Xp <= player.XpToLevelUp)
        {
            bar.localScale = new Vector3(xpRatio, bar.localScale.y, bar.localScale.z);
            point.text = player.Xp + " / " + player.XpToLevelUp;
        }
    }
}
