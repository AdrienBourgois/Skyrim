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

    private Dictionary<string, float> equipBonus = new Dictionary<string, float>();

    #endregion  

    public void SetCharacteristics(ACharacter _player)
    {
        CalcEquipBonusDic(_player);

        UnitCharacteristics.Attack = (Mathf.Exp(_player.UnitLevel / 8f) * UnitAttributes.Strength + equipBonus["Damage"]) * equipBonus["Attack"];
        UnitCharacteristics.Defense = (Mathf.Exp(_player.UnitLevel / 8f) * UnitAttributes.Constitution + equipBonus["Armor"] ) * equipBonus["Defense"];
        UnitCharacteristics.Weight = (UnitAttributes.Strength + _player.UnitLevel) * 10 * equipBonus["Weight"];
        UnitCharacteristics.MaxHealth = (Mathf.Exp(_player.UnitLevel / 6f) * UnitAttributes.Constitution + 100) * equipBonus["MaxHealth"];
        UnitCharacteristics.HealthRegeneration = UnitCharacteristics.MaxHealth / (50 - UnitAttributes.Constitution * 0.25f) * equipBonus["HealthRegeneration"];
        UnitCharacteristics.MaxMana = UnitAttributes.Intelligence * 10 * equipBonus["MaxMana"];
        UnitCharacteristics.SpellPower = (1 + (float)_player.UnitLevel * UnitAttributes.Intelligence / 100) * equipBonus["SpellPower"];
        UnitCharacteristics.Precision = Mathf.Min(100, 100 - (50 - (UnitCharacteristics.Weight - UnitCharacteristics.PlayerWeight) / 10) + UnitAttributes.Dexterity / 3) * equipBonus["Precision"];
        UnitCharacteristics.AttackSpeed = (1 + ((float)_player.UnitLevel + UnitAttributes.Dexterity / 2) / 100) * equipBonus["AttackSpeed"];

        characteristics.UpdateCharacDict();
    }

    public Characteristics SimulateCharac(int _level,float _playerWeigth, int _strength, int _constit, int _intel, int _dexterity)
    {
        Characteristics characs = new Characteristics(0)
        {
            Attack = (Mathf.Exp(_level/8f)*_strength + equipBonus["Damage"])*equipBonus["Attack"],
            Defense = (Mathf.Exp(_level/8f)*_constit + equipBonus["Armor"])*equipBonus["Defense"],
            Weight = (_strength + _level)*10*equipBonus["Weight"],
            MaxHealth = (Mathf.Exp(_level/6f)*_constit + 100)*equipBonus["MaxHealth"]
        };

        characs.HealthRegeneration = characs.MaxHealth / (50 - _constit * 0.25f) * equipBonus["HealthRegeneration"];
        characs.MaxMana = _intel * 10 * equipBonus["MaxMana"];
        characs.SpellPower = (1 + (float)_level * _intel / 100) * equipBonus["SpellPower"];
        characs.Precision = Mathf.Min(100, 100 - (50 - (characs.Weight - _playerWeigth) / 10) + _dexterity / 3) * equipBonus["Precision"];
        characs.AttackSpeed = (1 + ((float)_level + _dexterity / 2) / 100) * equipBonus["AttackSpeed"];

        characs.UpdateCharacDict();

        return characs;
    }

    private Dictionary<string, float> CalcEquipBonusDic(ACharacter _player)
    {
        if (equipBonus.Count == 0)
            InitEquibBonusDic();

        Weapon weapon = _player.RightHand ?? new Weapon();
        Shield shield = _player.LeftHand ?? new Shield();
        Helmet helmet = _player.Helmet ?? new Helmet();
        Torso chest = _player.Torso ?? new Torso();
        Boots boots = _player.Boots ?? new Boots();

        equipBonus["Damage"] = weapon.Damage;
        equipBonus["Armor"] = shield.Defense 
                                + helmet.Defense
                                + chest.Defense
                                + boots.Defense;

        Characteristics weaponC = weapon.Characteristics;
        Characteristics shieldC = shield.Characteristics;
        Characteristics helmetC = helmet.Characteristics;
        Characteristics chestC = chest.Characteristics;
        Characteristics bootsC = boots.Characteristics;

        Debug.Log(weaponC.MaxHealth);

        equipBonus["Attack"] = 1f + (weaponC.Attack + shieldC.Attack + helmetC.Attack + chestC.Attack + bootsC.Attack);// 100;
        equipBonus["Defense"] = 1f + (weaponC.Defense + shieldC.Defense + helmetC.Defense + chestC.Defense + bootsC.Defense);// 100;
        equipBonus["Weight"] = 1f + (weaponC.Weight + shieldC.Weight + helmetC.Weight + chestC.Weight + bootsC.Weight);// 100;
        equipBonus["MaxHealth"] = 1f + (weaponC.MaxHealth + shieldC.MaxHealth + helmetC.MaxHealth + chestC.MaxHealth + bootsC.MaxHealth);// 100;
        equipBonus["HealthRegeneration"] = 1f + (weaponC.HealthRegeneration + shieldC.HealthRegeneration + helmetC.HealthRegeneration + chestC.HealthRegeneration + bootsC.HealthRegeneration);// 100;
        equipBonus["MaxMana"] = 1f + (weaponC.MaxMana + shieldC.MaxMana + helmetC.MaxMana + chestC.MaxMana + bootsC.MaxMana);// 100;
        equipBonus["SpellPower"] = 1f + (weaponC.SpellPower + shieldC.SpellPower + helmetC.SpellPower + chestC.SpellPower + bootsC.SpellPower);// 100;
        equipBonus["Precision"] = 1f + (weaponC.Precision + shieldC.Precision + helmetC.Precision + chestC.Precision + bootsC.Precision);// 100;
        equipBonus["AttackSpeed"] = 1f + (weaponC.AttackSpeed + shieldC.AttackSpeed + helmetC.AttackSpeed + chestC.AttackSpeed + bootsC.AttackSpeed);// 100;

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
