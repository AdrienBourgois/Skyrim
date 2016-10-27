using UnityEngine;

public class XPBar : Bar
{
    private void Update()
    {
      //  if (player != null)
        //{
            float xp_ratio = player.Xp / player.XpToLevelUp;

            if (player.Xp <= player.XpToLevelUp)
            {
                bar.localScale = new Vector3(xp_ratio, bar.localScale.y, bar.localScale.z);
                point.text = player.Xp.ToString() + " / " + player.XpToLevelUp.ToString();
            }
        //}
    }
}
