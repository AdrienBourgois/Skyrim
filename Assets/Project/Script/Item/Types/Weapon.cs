using UnityEngine;

public class Weapon : Item, ITypeItem {

    protected enum weapon_type
    {
        Sword,
        Axe
    }
    protected weapon_type WeaponType;
    private Characteristics characteristics = new Characteristics();

    public int Damage { get; protected set; }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n=====================================" +
            "\nDamage : " + Damage
            + GetBonusInformations();
    }

    private string GetBonusInformations()
    {
        string bonus_informations = "\n================ BONUS ===============";
        if (characteristics.Attack != 0)
            bonus_informations += "\nAttack : +" + (int)(characteristics.Attack * 100) + "%";
        if (characteristics.AttackSpeed != 0)
            bonus_informations += "\nAttackSpeed : +" + (int)(characteristics.AttackSpeed * 100) + "%";
        if (characteristics.Defense != 0)
            bonus_informations += "\nDefense : +" + (int)(characteristics.Defense * 100) + "%";
        if (characteristics.Health != 0)
            bonus_informations += "\nHealth : +" + (int)(characteristics.Health * 100) + "%";
        if (characteristics.Health != 0)
            bonus_informations += "\nHealth : +" + (int)(characteristics.Health * 100) + "%";
        if (characteristics.Mana != 0)
            bonus_informations += "\nMana : +" + (int)(characteristics.Mana * 100) + "%";
        if (characteristics.Precision != 0)
            bonus_informations += "\nPrecision : +" + (int)(characteristics.Precision * 100) + "%";
        if (characteristics.SpellPower != 0)
            bonus_informations += "\nSpellPower : +" + (int)(characteristics.SpellPower * 100) + "%";
        if (characteristics.Weight != 0)
            bonus_informations += "\nWeight : +" + (int)(characteristics.Weight * 100) + "%";

        return bonus_informations;
    }

    protected void SetRandAttributes()
    {
        int rarity = (int)Rarity + 1;

        characteristics.Defense += Random.Range(0, rarity * 5) * 0.01f;
        characteristics.Health += Random.Range(0, rarity * 5) * 0.01f;
        characteristics.MaxHealth += Random.Range(0, rarity * 5) * 0.01f;
    }
}
