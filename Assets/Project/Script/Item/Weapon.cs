using UnityEngine;
using System.Collections;
using System;

public class Weapon : Item, IEquipableItem, IInstanciableItem
{
    int damage_value = 0;

    public void Equip()
    {
        throw new NotImplementedException();
    }

    public void Instantiate()
    {
        int power_lvl = 0;

        switch (Rarity)
        {
            case item_rarity.common:
                power_lvl = 0;
                break;
            case item_rarity.uncommon:
                power_lvl = 1;
                break;
            case item_rarity.rare:
                power_lvl = 2;
                break;
            case item_rarity.epic:
                power_lvl = 3;
                break;
            case item_rarity.legendary:
                power_lvl = 4;
                break;
        }

        damage_value = UnityEngine.Random.Range(power_lvl * 20, (power_lvl + 1) * 20);
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() + "\n=====================================\nDamage : " + damage_value;
    }
}
