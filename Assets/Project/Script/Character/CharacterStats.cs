using UnityEngine;

public class CharacterStats
{
    #region Stats
    private Characteristics baseCharacteristics = new Characteristics();
    public Characteristics BaseCharacteristics
    {
        get { return baseCharacteristics; }
    }

    private Attributes attributes = new Attributes();
    public Attributes UnitAttributes
    {
        get { return attributes; }
    }

    #endregion  

    public void SetCharacteristics(ACharacter player)
    {
        BaseCharacteristics.Attack = Mathf.Exp((player.UnitLevel / 8f)) * UnitAttributes.Strength;
        BaseCharacteristics.Defense = Mathf.Exp((player.UnitLevel / 8f)) * UnitAttributes.Constitution;
        BaseCharacteristics.Weight = (UnitAttributes.Strength + player.UnitLevel) * 10;
        BaseCharacteristics.MaxHealth = Mathf.Exp(player.UnitLevel / 6f) * UnitAttributes.Constitution + 100;
        BaseCharacteristics.HealthRegeneration = BaseCharacteristics.MaxHealth / (50 - (UnitAttributes.Constitution * 0.25f));
        BaseCharacteristics.MaxMana = UnitAttributes.Intelligence * 10;
        BaseCharacteristics.SpellPower = 1 + ((float)player.UnitLevel * UnitAttributes.Intelligence) / 100;
        BaseCharacteristics.Precision = Mathf.Min(100, 100 - (50 - (BaseCharacteristics.Weight - BaseCharacteristics.PlayerWeight) / 10) + UnitAttributes.Dexterity / 3);
        BaseCharacteristics.AttackSpeed = 1 + ((float)player.UnitLevel + (UnitAttributes.Dexterity / 2)) / 100;

        baseCharacteristics.UpdateCharacDict();
    }

    public void DisplayChara()
    {
        Debug.Log(BaseCharacteristics.Attack);
        Debug.Log(BaseCharacteristics.Defense);
        Debug.Log(BaseCharacteristics.Weight);
        Debug.Log(BaseCharacteristics.Health);
        Debug.Log(BaseCharacteristics.HealthRegeneration);
        Debug.Log(BaseCharacteristics.Mana);
        Debug.Log(BaseCharacteristics.SpellPower);
        Debug.Log(BaseCharacteristics.Precision);
        Debug.Log(BaseCharacteristics.AttackSpeed.ToString("F2"));

    }

    public Characteristics SimulateCharac(int level,float playerWeigth, int strength, int constit, int intel, int dexterity)
    {
        Characteristics characs = new Characteristics();

        characs.Attack = Mathf.Exp((level / 8f)) * strength;
        characs.Defense = Mathf.Exp((level / 8f)) * constit;
        characs.Weight = (strength + level) * 10;
        characs.MaxHealth = Mathf.Exp(level / 6f) * constit + 100;
        characs.HealthRegeneration = characs.MaxHealth / (50 - (constit * 0.25f));
        characs.MaxMana = intel * 10;
        characs.SpellPower = 1 + ((float)level * intel) / 100;
        characs.Precision = Mathf.Min(100, 100 - (50 - (characs.Weight - playerWeigth) / 10) + dexterity / 3);
        characs.AttackSpeed = 1 + ((float)level + (dexterity / 2)) / 100;

        characs.UpdateCharacDict();

        return characs;
    }

}
