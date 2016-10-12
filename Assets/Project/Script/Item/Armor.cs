using System;
using UnityEngine;

public class Armor : Item, IEquipableItem, IInstanciableItem
{
    protected float armor_value = 0;

    public void Equip()
    {
        throw new NotImplementedException();
    }

    public void Instantiate()
    {
        type = item_type.armor;
        float power_lvl = (float)Rarity;

        float base_damage_value = Mathf.Floor(Mathf.Exp(Level / (6 - power_lvl / 8)) * 3);
        float min = base_damage_value * (1 - (RangeOfGeneration / 100));
        float max = base_damage_value * (1 + (RangeOfGeneration / 100));
        armor_value = UnityEngine.Random.Range(min, max);
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n=====================================" +
            "\nDamage : " + armor_value;
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Helmet");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
