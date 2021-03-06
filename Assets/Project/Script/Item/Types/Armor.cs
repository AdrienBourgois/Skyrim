﻿using UnityEngine;

public class Armor : Item, ITypeItem {

    [Useless]
    protected enum ArmorType
    {
        Helmet,
        Torso,
        Boots,
        Shield
    }

    private Characteristics characteristics = new Characteristics(0);
    public Characteristics Characteristics
    { get { return characteristics; } }

    public int Defense { get; protected set; }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n=====================================" +
            "\nArmor : " + Defense
            + GetBonusInformations();
    }

    private string GetBonusInformations()
    {
        string bonusInformations = "\n================ BONUS ===============";
        if (characteristics.Attack != 0)
            bonusInformations += "\nAttack : +" + (int)(characteristics.Attack * 100f) + "%";
        if (characteristics.AttackSpeed != 0)
            bonusInformations += "\nAttackSpeed : +" + (int)(characteristics.AttackSpeed * 100f) + "%";
        if (characteristics.Defense != 0)
            bonusInformations += "\nDefense : +" + (int)(characteristics.Defense * 100f) + "%";
        if (characteristics.MaxHealth != 0)
            bonusInformations += "\nHealth : +" + (int)(characteristics.MaxHealth * 100f) + "%";
        if (characteristics.HealthRegeneration != 0)
            bonusInformations += "\nHealthRegeneration : +" + (int)(characteristics.HealthRegeneration * 100f) + "%";
        if (characteristics.Mana != 0)
            bonusInformations += "\nMana : +" + (int)(characteristics.Mana * 100f) + "%";
        if (characteristics.Precision != 0)
            bonusInformations += "\nPrecision : +" + (int)(characteristics.Precision * 100f) + "%";
        if (characteristics.SpellPower != 0)
            bonusInformations += "\nSpellPower : +" + (int)(characteristics.SpellPower * 100f) + "%";
        if (characteristics.Weight != 0)
            bonusInformations += "\nWeight : +" + (int)(characteristics.Weight * 100f) + "%";

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
