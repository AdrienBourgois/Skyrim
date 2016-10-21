using UnityEngine;
using System;

public class Axe : Weapon, IEquipableItem, IInstanciableItem
{
    public void Equip()
    {
        throw new NotImplementedException();
    }

    public void Instantiate()
    {
        Type = item_type.weapon;
        WeaponType = weapon_type.Axe;
        float power_lvl = (float)Rarity;

        SetRandAttributes();

        float base_damage_value = Mathf.Floor(Mathf.Exp(Level / (6 - power_lvl / 8)) * 15);
        float min = base_damage_value * (1 - (RangeOfGeneration / 100));
        float max = base_damage_value * (1 + (RangeOfGeneration / 100));
        Damage = (int)UnityEngine.Random.Range(min, max);

        Price = (int)(Mathf.Sqrt(Damage) * (10 + (power_lvl * 6)));
        Weight = 25 + (int)Rarity;

        PrefabPath = "Weapons/Axe";
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Axe");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
