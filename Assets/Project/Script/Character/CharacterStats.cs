using UnityEngine;
using System.Collections;

public class CharacterStats
{
    #region Stats
    private Characteristics characteristics;
    public Characteristics UnitCharacteristics
    {
        get { return characteristics; }
    }

    private Attributes attributes;
    public Attributes UnitAttributes
    {
        get { return attributes; }
    }

    #endregion  
    
    public void Init(int attack, int defense, float weight, int health, int mana, int spellPower, float precision, float attackSpeed)
    {
        characteristics = new Characteristics();
        attributes = new Attributes();

        characteristics.Init(attack, defense,
                             weight, health,
                             mana, spellPower,
                             precision, attackSpeed);
    }


    public void SetCharacteristics()
    {
        UnitCharacteristics.Weight = UnitAttributes.Strength * 10;
        UnitCharacteristics.Attack *= UnitAttributes.Strength;
        UnitCharacteristics.MaxHealth += UnitAttributes.Constitution;

    }
}
