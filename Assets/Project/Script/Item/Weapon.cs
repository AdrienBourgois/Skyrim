using UnityEngine;
using System.Collections;
using System;

public class Weapon : Item, IEquipableItem, IInstanciableItem
{
    float damage_value = 0;

    public void Equip()
    {
        throw new NotImplementedException();
    }

    public void Instantiate()
    {
        type = item_type.weapon;
        float power_lvl = (float)Rarity;

        float base_damage_value = Mathf.Floor(Mathf.Exp(Level / (6 - power_lvl / 8)) * 10);
        float min = base_damage_value * (1 - (RangeOfGeneration / 100));
        float max = base_damage_value * (1 + (RangeOfGeneration / 100));
        damage_value = UnityEngine.Random.Range(min, max);

    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n====================================="+
            "\nDamage : " + damage_value;
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Sword");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
