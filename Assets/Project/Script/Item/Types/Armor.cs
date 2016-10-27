using UnityEngine;

public class Armor : Item, ITypeItem {

    protected enum armor_type
    {
        Helmet,
        Torso,
        Boots,
        Shield
    }
    protected armor_type ArmorType;
    private Characteristics characteristics = new Characteristics(0);
    public Characteristics Characteristics
    { get { return characteristics; } }

    private int defenseValue;
    public int Defense
    {
        get { return defenseValue; }
        protected set { defenseValue = value; }
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n=====================================" +
            "\nArmor : " + defenseValue
            + GetBonusInformations();
    }

    private string GetBonusInformations()
    {
        string bonusInformations = "\n================ BONUS ===============";
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

        return bonusInformations;
    }

    protected void SetRandAttributes()
    {
        int rarity = (int)Rarity + 1;

        characteristics.Attack += Random.Range(0, rarity * 5) * 0.01f;
        characteristics.AttackSpeed += Random.Range(0, rarity * 5) * 0.01f;
        characteristics.Precision += Random.Range(0, rarity * 5) * 0.01f;
    }
}
