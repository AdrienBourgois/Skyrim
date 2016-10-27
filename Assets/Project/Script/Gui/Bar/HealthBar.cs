using UnityEngine;

public class HealthBar : Bar {
    private void Update ()
    {
        
        Characteristics playerStats = player.CharacterStats.UnitCharacteristics;
        float lifeRatio = playerStats.Health / playerStats.MaxHealth;


        if (playerStats.Health >= 0)
        {
            bar.localScale = new Vector3(lifeRatio, bar.localScale.y, bar.localScale.z);
            point.text = playerStats.Health.ToString("0") + " / " + playerStats.MaxHealth.ToString("0");
        }
   }
}
