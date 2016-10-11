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
        type = item_type.armor;
        int power_lvl = (int)type;

        armor_value = UnityEngine.Random.Range(power_lvl * 20, (power_lvl + 1) * 20);
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n=====================================" +
            "\nDamage : " + armor_value;
    }
}
