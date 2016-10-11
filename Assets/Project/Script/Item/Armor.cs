using System;

public class Armor : Item, IEquipableItem, IInstanciableItem
{
    protected int armor_value = 0;

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

        armor_value = UnityEngine.Random.Range(power_lvl * 20, (power_lvl + 1) * 20);
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() + "\n=====================================\nDamage : " + armor_value;
    }
}
