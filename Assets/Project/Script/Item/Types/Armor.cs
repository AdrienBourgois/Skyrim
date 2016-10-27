using UnityEngine;

public class Armor : Item, ITypeItem {

    protected enum armor_type
    {
        Helmet,
        Torso,
        Boots,
        Shield
    }
    protected armor_type armorType;
    private Characteristics characteristics = new Characteristics();

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
            bonusInformations += "\nAttack : +" + (int)(characteristics.Attack * 100) + "%";
        if (characteristics.AttackSpeed != 0)
            bonusInformations += "\nAttackSpeed : +" + (int)(characteristics.AttackSpeed * 100) + "%";
        if (characteristics.Defense != 0)
            bonusInformations += "\nDefense : +" + (int)(characteristics.Defense * 100) + "%";
        if (characteristics.Health != 0)
            bonusInformations += "\nHealth : +" + (int)(characteristics.Health * 100) + "%";
        if (characteristics.Health != 0)
            bonusInformations += "\nHealth : +" + (int)(characteristics.Health * 100) + "%";
        if (characteristics.Mana != 0)
            bonusInformations += "\nMana : +" + (int)(characteristics.Mana * 100) + "%";
        if (characteristics.Precision != 0)
            bonusInformations += "\nPrecision : +" + (int)(characteristics.Precision * 100) + "%";
        if (characteristics.SpellPower != 0)
            bonusInformations += "\nSpellPower : +" + (int)(characteristics.SpellPower * 100) + "%";
        if (characteristics.Weight != 0)
            bonusInformations += "\nWeight : +" + (int)(characteristics.Weight * 100) + "%";

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
