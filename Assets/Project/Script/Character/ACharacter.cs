using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
public abstract class ACharacter : MonoBehaviour
{    
    private int unitMaxLevel;
    public int MaxUnitLevel
    {
        get { return unitMaxLevel; }
        protected set { unitMaxLevel = value; }
    }

    #region Serialized Fields
    [SerializeField]
    private string unitName;
    public string UnitName
    {
        get { return unitName; }
        protected set { unitName = value; }
    }

    [SerializeField]
    private int unitLevel;
    public int UnitLevel
    {
        get { return unitLevel; }
        protected set { unitLevel = value; }
    }    

    [SerializeField]
    private float jumpEfficiency = 5.5f;
    public float JumpEfficiency
    {
        get { return jumpEfficiency; }
        protected set { jumpEfficiency = value; }
    }

    [SerializeField]
    private float baseMoveSpeed = 3f;
    public float MoveSpeed
    {
        get { return baseMoveSpeed; }
        protected set { baseMoveSpeed = value; }
    }

    [SerializeField]
    private int baseAttack;
    [SerializeField]
    private int baseDefense;
    [SerializeField]
    private float baseWeight;
    [SerializeField]
    private float baseHealth = 100f;
    [SerializeField]
    private int baseMana = 100;
    [SerializeField]
    private int baseSpellPower;
    [SerializeField]
    private float basePrecision;
    [SerializeField]
    private float baseAttackSpeed;
    #endregion

    #region Stats & Inventory
    private CharacterStats characterStats;
    public CharacterStats CharacterStats
    {
        get { return characterStats; }
    }
    
    /*
    private Inventory inventory;
    public Inventory UnitInventory
    {
        get { return inventory; }
    }*/
    #endregion
    
    protected virtual void Start()
    {
        characterStats = new CharacterStats();
        characterStats.Init(baseAttack, baseDefense,
                            baseWeight, baseHealth,
                            baseMana, baseSpellPower,
                            basePrecision, baseAttackSpeed);

        characterStats.SetCharacteristics(this);        
    }
}
