using UnityEngine;

public class Weapon : Item, ITypeItem {

    protected enum weapon_type
    {
        Sword,
        Axe
    }
    protected weapon_type WeaponType;
    private Characteristics characteristics = new Characteristics(0);
    public Characteristics Characteristics
    { get { return characteristics; } }

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
            bonus_informations += "\nAttack : +" + (int)(characteristics.Attack * 100f) + "%";
        if (characteristics.AttackSpeed != 0)
            bonus_informations += "\nAttackSpeed : +" + (int)(characteristics.AttackSpeed * 100f) + "%";
        if (characteristics.Defense != 0)
            bonus_informations += "\nDefense : +" + (int)(characteristics.Defense * 100f) + "%";
        if (characteristics.MaxHealth != 0)
            bonus_informations += "\nHealth : +" + (int)(characteristics.MaxHealth * 100f) + "%";
        if (characteristics.HealthRegeneration != 0)
            bonus_informations += "\nHealthRegeneration : +" + (int)(characteristics.HealthRegeneration * 100f) + "%";
        if (characteristics.Mana != 0)
            bonus_informations += "\nMana : +" + (int)(characteristics.Mana * 100f) + "%";
        if (characteristics.Precision != 0)
            bonus_informations += "\nPrecision : +" + (int)(characteristics.Precision * 100f) + "%";
        if (characteristics.SpellPower != 0)
            bonus_informations += "\nSpellPower : +" + (int)(characteristics.SpellPower * 100f) + "%";
        if (characteristics.Weight != 0)
            bonus_informations += "\nWeight : +" + (int)(characteristics.Weight * 100f) + "%";

        return bonus_informations;
    }

    protected void SetRandAttributes()
    {
        int rarity = (int)Rarity + 1;

        characteristics.Defense += Random.Range(0, rarity * 5) * 0.01f;
        characteristics.HealthRegeneration += Random.Range(0, rarity * 5) * 0.01f;
        characteristics.MaxHealth += Random.Range(0, rarity * 5) * 0.01f;
    }
}
