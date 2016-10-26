using UnityEngine;

public class HealthBar : Bar {
    private void Update ()
    {
        
        Characteristics player_stats = player.CharacterStats.BaseCharacteristics;
        float life_ratio = player_stats.Health / player_stats.MaxHealth;


        if (player_stats.Health >= 0)
        {
            bar.localScale = new Vector3(life_ratio, bar.localScale.y, bar.localScale.z);
            point.text = player_stats.Health.ToString("0");
        }
   }
}
