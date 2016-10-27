using UnityEngine;
using System.Collections.Generic;

public class CharacterStats
{
    #region Stats
    private Characteristics characteristics = new Characteristics(0);
    public Characteristics UnitCharacteristics
    {
        get { return characteristics; }
    }

    private Attributes attributes = new Attributes();
    public Attributes UnitAttributes
    {
        get { return attributes; }
    }

    Dictionary<string, float> equipBonus = new Dictionary<string, float>();

    #endregion  

    public void SetCharacteristics(ACharacter _player)
    {
        CalcEquipBonusDic(_player);

        UnitCharacteristics.Attack = ((Mathf.Exp((_player.UnitLevel / 8f)) * UnitAttributes.Strength) + equipBonus["Damage"]) * equipBonus["Attack"];
        UnitCharacteristics.Defense = ((Mathf.Exp((_player.UnitLevel / 8f)) * UnitAttributes.Constitution) + equipBonus["Armor"] ) * equipBonus["Defense"];
        UnitCharacteristics.Weight = ((UnitAttributes.Strength + _player.UnitLevel) * 10) * equipBonus["Weight"];
        UnitCharacteristics.MaxHealth = (Mathf.Exp(_player.UnitLevel / 6f) * UnitAttributes.Constitution + 100) * equipBonus["MaxHealth"];
        UnitCharacteristics.HealthRegeneration = (UnitCharacteristics.MaxHealth / (50 - (UnitAttributes.Constitution * 0.25f))) * equipBonus["HealthRegeneration"];
        UnitCharacteristics.MaxMana = (UnitAttributes.Intelligence * 10) * equipBonus["MaxMana"];
        UnitCharacteristics.SpellPower = (1 + ((float)_player.UnitLevel * UnitAttributes.Intelligence) / 100) * equipBonus["SpellPower"];
        UnitCharacteristics.Precision = (Mathf.Min(100, 100 - (50 - (UnitCharacteristics.Weight - UnitCharacteristics.PlayerWeight) / 10) + UnitAttributes.Dexterity / 3)) * equipBonus["Precision"];
        UnitCharacteristics.AttackSpeed = (1 + ((float)_player.UnitLevel + (UnitAttributes.Dexterity / 2)) / 100) * equipBonus["AttackSpeed"];

        characteristics.UpdateCharacDict();
    }

    public Characteristics SimulateCharac(int level,float playerWeigth, int strength, int constit, int intel, int dexterity)
    {
        Characteristics characs = new Characteristics(0);

        characs.Attack = ((Mathf.Exp((level / 8f)) * strength) + equipBonus["Damage"]) * equipBonus["Attack"];
        characs.Defense = ((Mathf.Exp((level / 8f)) * constit) + equipBonus["Armor"]) * equipBonus["Defense"];
        characs.Weight = ((strength + level) * 10) * equipBonus["Weight"];
        characs.MaxHealth = (Mathf.Exp(level / 6f) * constit + 100) * equipBonus["MaxHealth"];
        characs.HealthRegeneration = (characs.MaxHealth / (50 - (constit * 0.25f))) * equipBonus["HealthRegeneration"];
        characs.MaxMana = (intel * 10) * equipBonus["MaxMana"];
        characs.SpellPower = (1 + ((float)level * intel) / 100) * equipBonus["SpellPower"];
        characs.Precision = (Mathf.Min(100, 100 - (50 - (characs.Weight - playerWeigth) / 10) + dexterity / 3)) * equipBonus["Precision"];
        characs.AttackSpeed = (1 + ((float)level + (dexterity / 2)) / 100) * equipBonus["AttackSpeed"];

        characs.UpdateCharacDict();

        return characs;
    }

    private Dictionary<string, float> CalcEquipBonusDic(ACharacter player)
    {
        if (equipBonus.Count == 0)
            InitEquibBonusDic();

        Weapon weapon = player.RightHand != null ? player.RightHand : new Weapon();
        Shield shield = player.LeftHand != null ? player.LeftHand : new Shield();
        Helmet helmet = player.Helmet != null ? player.Helmet : new Helmet();
        Torso chest = player.Torso != null ? player.Torso : new Torso();
        Boots boots = player.Boots != null ? player.Boots : new Boots();

        equipBonus["Damage"] = weapon.Damage;
        equipBonus["Armor"] = shield.Defense 
                                + helmet.Defense
                                + chest.Defense
                                + boots.Defense;

        Characteristics weapon_c = weapon != null ? weapon.Characteristics : new Characteristics(0);
        Characteristics shield_c = shield != null ? shield.Characteristics : new Characteristics(0);
        Characteristics helmet_c = helmet != null ? helmet.Characteristics : new Characteristics(0);
        Characteristics chest_c = chest != null ? chest.Characteristics : new Characteristics(0);
        Characteristics boots_c = boots != null ? boots.Characteristics : new Characteristics(0);

        equipBonus["Attack"] = 1f + (weapon_c.Attack + shield_c.Attack + helmet_c.Attack + chest_c.Attack + boots_c.Attack);// 100;
        equipBonus["Defense"] = 1f + (weapon_c.Defense + shield_c.Defense + helmet_c.Defense + chest_c.Defense + boots_c.Defense);// 100;
        equipBonus["Weight"] = 1f + (weapon_c.Weight + shield_c.Weight + helmet_c.Weight + chest_c.Weight + boots_c.Weight);// 100;
        equipBonus["MaxHealth"] = 1f + (weapon_c.MaxHealth + shield_c.MaxHealth + helmet_c.MaxHealth + chest_c.MaxHealth + boots_c.MaxHealth);// 100;
        equipBonus["HealthRegeneration"] = 1f + (weapon_c.HealthRegeneration + shield_c.HealthRegeneration + helmet_c.HealthRegeneration + chest_c.HealthRegeneration + boots_c.HealthRegeneration);// 100;
        equipBonus["MaxMana"] = 1f + (weapon_c.MaxMana + shield_c.MaxMana + helmet_c.MaxMana + chest_c.MaxMana + boots_c.MaxMana);// 100;
        equipBonus["SpellPower"] = 1f + (weapon_c.SpellPower + shield_c.SpellPower + helmet_c.SpellPower + chest_c.SpellPower + boots_c.SpellPower);// 100;
        equipBonus["Precision"] = 1f + (weapon_c.Precision + shield_c.Precision + helmet_c.Precision + chest_c.Precision + boots_c.Precision);// 100;
        equipBonus["AttackSpeed"] = 1f + (weapon_c.AttackSpeed + shield_c.AttackSpeed + helmet_c.AttackSpeed + chest_c.AttackSpeed + boots_c.AttackSpeed);// 100;

        return equipBonus;
    }

    private void InitEquibBonusDic()
    {
        equipBonus.Add("Damage", 0);
        equipBonus.Add("Armor", 0);
        equipBonus.Add("Attack", 0);
        equipBonus.Add("Defense", 0);
        equipBonus.Add("Weight", 0);
        equipBonus.Add("MaxHealth", 0);
        equipBonus.Add("HealthRegeneration", 0);
        equipBonus.Add("MaxMana", 0);
        equipBonus.Add("SpellPower", 0);
        equipBonus.Add("Precision", 0);
        equipBonus.Add("AttackSpeed", 0);
    }
}
