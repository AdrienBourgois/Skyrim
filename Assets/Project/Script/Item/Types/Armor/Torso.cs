using System;
using UnityEngine;

public class Torso : Armor, IEquipableItem, IInstanciableItem
{
    public void Equip()
    {
        throw new NotImplementedException();
    }

    public void Instantiate()
    {
        Type = item_type.armor;
        ArmorType = armor_type.Torso;
        float power_lvl = (float)Rarity;

        SetRandAttributes();

        float base_armor_value = Mathf.Floor(Mathf.Exp(Level / (6 - power_lvl / 8)) * 6);
        float min = base_armor_value * (1 - (RangeOfGeneration / 100));
        float max = base_armor_value * (1 + (RangeOfGeneration / 100));
        Defense = UnityEngine.Random.Range(min, max);

        Price = (int)(Mathf.Sqrt(Defense) * (10 + (power_lvl * 6)));

        Weight = 20 + (int)Rarity;
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Torso");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
