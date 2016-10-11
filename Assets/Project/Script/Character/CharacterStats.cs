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

    void Start ()
    {

    }
	
	void Update () {
	
	}

    public void SetCharacteristics()
    {
        UnitCharacteristics.Weight = UnitAttributes.Strength * 10;
        UnitCharacteristics.Attack *= UnitAttributes.Strength;
        UnitCharacteristics.MaxHealth += UnitAttributes.Constitution;

    }
}
