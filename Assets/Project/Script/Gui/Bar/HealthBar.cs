using UnityEngine;

public class HealthBar : Bar
{
    float lastHealthValue = 0f;
    float lastMaxHealthValue = 0f;

    private void Update ()
    {
        Characteristics playerStats = player.CharacterStats.UnitCharacteristics;
        float lifeRatio = playerStats.Health / playerStats.MaxHealth;

        lastHealthValue = playerStats.Health;
        lastMaxHealthValue = playerStats.MaxHealth;

        if (playerStats.Health >= 0 && (lastHealthValue != playerStats.Health || lastMaxHealthValue != playerStats.MaxHealth))
        {
            bar.localScale = new Vector3(lifeRatio, bar.localScale.y, bar.localScale.z);
            point.text = playerStats.Health.ToString("0") + " / " + playerStats.MaxHealth.ToString("0");
        }
   }
}
