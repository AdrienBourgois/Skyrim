using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
public abstract class ACharacter : APausableObject
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
    private int unitLevel = 1;
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
    #endregion

    #region Stats & Inventory & Spell
    private CharacterStats characterStats = new CharacterStats();
    public CharacterStats CharacterStats
    {
        get { return characterStats; }
    }

    private Inventory inventory = new Inventory();
    public Inventory UnitInventory
    {
        get { return inventory; }
    }

    private SpellInventory spells = new SpellInventory();
    public SpellInventory UnitSpells
    {
        get { return spells; }
    }
    #endregion
   

    protected virtual void Start()
    {
        characterStats.SetCharacteristics(this);
        CharacterStats.UnitCharacteristics.RegenFullHealthAndMana();
    }
}
