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
    
    public void Init(float attack, float defense, float weight, float health, float mana, float spellPower, float precision, float attackSpeed)
    {
        characteristics = new Characteristics();
        attributes = new Attributes();

        characteristics.Init(attack, defense,
                             weight, health,
                             mana, spellPower,
                             precision, attackSpeed);

        
    }


    public void SetCharacteristics(ACharacter player)
    {
        Debug.Log("Level : " + player.UnitLevel);


        UnitCharacteristics.Attack = Mathf.Exp(((float)player.UnitLevel / 8)) * UnitAttributes.Strength;
        UnitCharacteristics.Defense = Mathf.Exp(((float)player.UnitLevel / 8)) * UnitAttributes.Constitution;
        UnitCharacteristics.Weight = (UnitAttributes.Strength + player.UnitLevel) * 10;
        UnitCharacteristics.Health = Mathf.Exp((float)player.UnitLevel / 6) + UnitAttributes.Constitution + 100;
        UnitCharacteristics.HealthRegeneration = Mathf.Round(UnitCharacteristics.Health / (50 - (UnitAttributes.Constitution * 0.25f)));
        UnitCharacteristics.Mana = UnitAttributes.Intelligence * 10;
        UnitCharacteristics.SpellPower = 1 + ((float)player.UnitLevel * UnitAttributes.Intelligence) / 100;
        UnitCharacteristics.Precision = Mathf.Min(100, 100 - (50 - (UnitCharacteristics.Weight - UnitCharacteristics.PlayerWeight) / 10) + UnitAttributes.Dexterity / 3);
        UnitCharacteristics.AttackSpeed = 1 + ((float)player.UnitLevel + (UnitAttributes.Dexterity / 2)) / 100;
    }

    public void DisplayChara()
    {
        Debug.Log(UnitCharacteristics.Attack);
        Debug.Log(UnitCharacteristics.Defense);
        Debug.Log(UnitCharacteristics.Weight);
        Debug.Log(UnitCharacteristics.Health);
        Debug.Log(UnitCharacteristics.HealthRegeneration);
        Debug.Log(UnitCharacteristics.Mana);
        Debug.Log(UnitCharacteristics.SpellPower);
        Debug.Log(UnitCharacteristics.Precision);
        Debug.Log(UnitCharacteristics.AttackSpeed.ToString("F2"));

    }

}
