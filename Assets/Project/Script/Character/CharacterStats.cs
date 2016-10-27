using UnityEngine;

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

    public void SetCharacteristics(ACharacter _player)
    {
        UnitCharacteristics.Attack = Mathf.Exp((_player.UnitLevel / 8f)) * UnitAttributes.Strength;
        UnitCharacteristics.Defense = Mathf.Exp((_player.UnitLevel / 8f)) * UnitAttributes.Constitution;
        UnitCharacteristics.Weight = (UnitAttributes.Strength + _player.UnitLevel) * 10;
        UnitCharacteristics.MaxHealth = Mathf.Exp(_player.UnitLevel / 6f) * UnitAttributes.Constitution + 100;
        UnitCharacteristics.HealthRegeneration = UnitCharacteristics.MaxHealth / (50 - (UnitAttributes.Constitution * 0.25f));
        UnitCharacteristics.MaxMana = UnitAttributes.Intelligence * 10;
        UnitCharacteristics.SpellPower = 1 + ((float)_player.UnitLevel * UnitAttributes.Intelligence) / 100;
        UnitCharacteristics.Precision = Mathf.Min(100, 100 - (50 - (UnitCharacteristics.Weight - UnitCharacteristics.PlayerWeight) / 10) + UnitAttributes.Dexterity / 3);
        UnitCharacteristics.AttackSpeed = 1 + ((float)_player.UnitLevel + (UnitAttributes.Dexterity / 2)) / 100;

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

    public Characteristics SimulateCharac(int _level,float _playerWeigth, int _strength, int _constit, int _intel, int _dexterity)
    {
        Characteristics characs = new Characteristics
        {
            Attack = Mathf.Exp((_level/8f))*_strength,
            Defense = Mathf.Exp((_level/8f))*_constit,
            Weight = (_strength + _level)*10,
            MaxHealth = Mathf.Exp(_level/6f)*_constit + 100
        };

        characs.HealthRegeneration = characs.MaxHealth / (50 - (_constit * 0.25f));
        characs.MaxMana = _intel * 10;
        characs.SpellPower = 1 + ((float)_level * _intel) / 100;
        characs.Precision = Mathf.Min(100, 100 - (50 - (characs.Weight - _playerWeigth) / 10) + _dexterity / 3);
        characs.AttackSpeed = 1 + ((float)_level + (_dexterity / 2)) / 100;

        characs.UpdateCharacDict();

        return characs;
    }

}
