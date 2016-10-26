using System;
using UnityEngine;

public class Boots : Armor, IEquipableItem, IInstanciableItem
{
    public void Equip()
    {
        Player player = LevelManager.Instance.Player;
        if (player.Boots != null)
            player.CharacterStats.BaseCharacteristics.Defense -= player.Boots.Defense;

        player.Boots = this;
        player.CharacterStats.BaseCharacteristics.Defense += Defense;
        //need to adjust characteristics for equipable item  
    }

    public void Instantiate()
    {
        Type = item_type.armor;
        ArmorType = armor_type.Boots;
        float power_lvl = (float)Rarity;

        SetRandAttributes();

        float base_armor_value = Mathf.Floor(Mathf.Exp(Level / (6 - power_lvl / 8)) * 2);
        float min = base_armor_value * (1 - (RangeOfGeneration / 100));
        float max = base_armor_value * (1 + (RangeOfGeneration / 100));
        Defense = (int)UnityEngine.Random.Range(min, max);

        Price = (int)(Mathf.Sqrt(Defense) * (10 + (power_lvl * 6)));

        Weight = 5 + (int)Rarity;
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Boots");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
