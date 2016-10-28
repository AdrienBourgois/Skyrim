using UnityEngine;

public class ManaBar : Bar {

    float lastManaValue = 0f;
    float lastMaxManaValue = 0f;

    private void Update()
    {
        Characteristics playerStats = player.CharacterStats.UnitCharacteristics;
        float manaRatio = playerStats.Mana / playerStats.MaxMana;

        lastManaValue = playerStats.Mana;
        lastMaxManaValue = playerStats.MaxMana;

        if (playerStats.Mana >= 0 && (lastManaValue != playerStats.Mana || lastMaxManaValue != playerStats.MaxMana))
        {
            bar.localScale = new Vector3(manaRatio, bar.localScale.y, bar.localScale.z);
            point.text = playerStats.Mana + " / " + playerStats.MaxMana.ToString("0");
        }
    }
}
