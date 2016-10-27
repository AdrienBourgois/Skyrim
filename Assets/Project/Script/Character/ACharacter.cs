using UnityEngine;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
public abstract class ACharacter : APausableObject
{    
    public delegate void DelegateWeapons(Item leftWeapon, Item rightWeapon);
    public event DelegateWeapons OnChangedWeapons;

    public int MaxUnitLevel { get; protected set; }

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
    private float baseMoveSpeed = 100f;
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

    private MagicInventory spells = new MagicInventory();
    public MagicInventory UnitSpells
    {
        get { return spells; }
    }
    #endregion
    
    public enum EquipType
    {
        None            = -1,
        SwordAndShield  =  0,
        Axe             =  1,
        COUNT,
    }

    private EquipType equipType = EquipType.None;
    public EquipType StuffType
    {
        get { return equipType; }
        protected set { equipType = value; }
    }

    protected virtual void Start()
    {
        characterStats.SetCharacteristics(this);
        CharacterStats.UnitCharacteristics.RegenFullHealthAndMana();

        // HACK: debug
        equipType = EquipType.SwordAndShield;
    }

    protected virtual void EquippedItemChanged()
    {
        // TODO: implemement event when equipped items changed
        //if (OnChangedWeapons != null)
        //  OnChangedWeapons.Invoke(inventory.);
    }
}
