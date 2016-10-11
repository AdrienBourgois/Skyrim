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

        damage_value = Mathf.Floor(Mathf.Exp(Level / (6 - power_lvl / 8)) * 10);
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n====================================="+
            "\nDamage : " + damage_value;
    }
}
