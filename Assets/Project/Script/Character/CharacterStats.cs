using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

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

    void Start () {

        DisplayChara();
	
	}
	
	void Update () {
	
	}

    public void SetCharacteristics(ACharacter player)
    {
        UnitCharacteristics.Attack = (int)Mathf.Exp((player.UnitLevel/8) * UnitAttributes.Strength);
        UnitCharacteristics.Defense = (int)Mathf.Exp((player.UnitLevel / 8) * UnitAttributes.Constitution);
        UnitCharacteristics.Weight = (UnitAttributes.Strength + player.UnitLevel) * 10;
        UnitCharacteristics.Health = (int)Mathf.Exp(player.UnitLevel / 6) + UnitAttributes.Constitution + 100;
        UnitCharacteristics.HealthRegeneration = (int)Mathf.Round(UnitCharacteristics.Health / (50 - (UnitAttributes.Constitution * 0.25f)));
        UnitCharacteristics.Mana = UnitAttributes.Intelligence * 10;
        UnitCharacteristics.SpellPower = 1 + (player.UnitLevel * UnitAttributes.Intelligence) / 100;
        UnitCharacteristics.Precision = 100 - (50 - (UnitCharacteristics.Weight - UnitAttributes.Dexterity) / 10);
        UnitCharacteristics.AttackSpeed = 1 + (player.UnitLevel + (UnitAttributes.Dexterity / 2)) / 75;
    }

    void DisplayChara()
    {
        print(UnitCharacteristics.Attack);
        print(UnitCharacteristics.Defense);
        print(UnitCharacteristics.Weight);
        print(UnitCharacteristics.Health);
        print(UnitCharacteristics.HealthRegeneration);
        print(UnitCharacteristics.Mana);
        print(UnitCharacteristics.SpellPower);
        print(UnitCharacteristics.Precision);
        print(UnitCharacteristics.AttackSpeed);
        

    }

}
