using UnityEngine;
using System.Collections.Generic;

public class CharacterStats
{
    #region Stats
    private Characteristics characteristics = new Characteristics();
    public Characteristics UnitCharacteristics
    {
        get { return characteristics; }
    }

    private Attributes attributes = new Attributes();
    public Attributes UnitAttributes
    {
        get { return attributes; }
    }

    #endregion  

    public void SetCharacteristics(ACharacter player)
    {
        UnitCharacteristics.Attack = Mathf.Exp(((float)player.UnitLevel / 8f)) * UnitAttributes.Strength;
        UnitCharacteristics.Defense = Mathf.Exp(((float)player.UnitLevel / 8f)) * UnitAttributes.Constitution;
        UnitCharacteristics.Weight = (UnitAttributes.Strength + player.UnitLevel) * 10;
        UnitCharacteristics.MaxHealth = Mathf.Exp((float)player.UnitLevel / 6f) * UnitAttributes.Constitution + 100;
        UnitCharacteristics.HealthRegeneration = UnitCharacteristics.MaxHealth / (50 - (UnitAttributes.Constitution * 0.25f));
        UnitCharacteristics.MaxMana = UnitAttributes.Intelligence * 10;
        UnitCharacteristics.SpellPower = 1 + ((float)player.UnitLevel * UnitAttributes.Intelligence) / 100;
        UnitCharacteristics.Precision = Mathf.Min(100, 100 - (50 - (UnitCharacteristics.Weight - UnitCharacteristics.PlayerWeight) / 10) + UnitAttributes.Dexterity / 3);
        UnitCharacteristics.AttackSpeed = 1 + ((float)player.UnitLevel + (UnitAttributes.Dexterity / 2)) / 100;

        characteristics.UpdateCharacDict();
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

    public Characteristics SimulateCharac(int level,float playerWeigth, int strength, int constit, int intel, int dexterity)
    {
        Characteristics characs = new Characteristics();

        characs.Attack = Mathf.Exp(((float)level / 8f)) * strength;
        characs.Defense = Mathf.Exp(((float)level / 8f)) * constit;
        characs.Weight = (strength + level) * 10;
        characs.MaxHealth = Mathf.Exp((float)level / 6f) * constit + 100;
        characs.HealthRegeneration = characs.MaxHealth / (50 - (constit * 0.25f));
        characs.MaxMana = intel * 10;
        characs.SpellPower = 1 + ((float)level * intel) / 100;
        characs.Precision = Mathf.Min(100, 100 - (50 - (characs.Weight - playerWeigth) / 10) + dexterity / 3);
        characs.AttackSpeed = 1 + ((float)level + (dexterity / 2)) / 100;

        characs.UpdateCharacDict();

        return characs;
    }

}
