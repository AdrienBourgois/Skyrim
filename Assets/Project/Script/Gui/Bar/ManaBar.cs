using UnityEngine;

public class ManaBar : Bar {
    private void Update()
    {
        Characteristics playerStats = player.CharacterStats.UnitCharacteristics;
        float manaRatio = playerStats.Mana / playerStats.MaxMana;

        if (playerStats.Mana >= 0)
        {
            bar.localScale = new Vector3(manaRatio, bar.localScale.y, bar.localScale.z);
            point.text = playerStats.Mana + " / " + playerStats.MaxMana.ToString("0");
        }
    }
}
