using UnityEngine;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
public abstract class ACharacter : APausableObject, IHitable
{    
    public delegate void DelegateWeapons(Item _leftWeapon, Item _rightWeapon);
    public event DelegateWeapons OnChangedWeapons;

    public int MaxUnitLevel { get; protected set; }

    #region Equipement

    protected Weapon rightHand;
    public Weapon RightHand
    { get { return rightHand; }
      set { rightHand = value; } }

    protected Shield leftHand;
    public Shield LeftHand
    { get { return leftHand; }
      set { leftHand = value; } }

    protected Helmet helmet;
    public Helmet Helmet
    { get { return helmet; }
      set { helmet = value; } }

    protected Torso torso;
    public Torso Torso
    { get { return torso; }
      set { torso = value; } }

    protected Boots boots;
    public Boots Boots
    { get { return boots; }
      set { boots = value; } }

    #endregion

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
    private float jumpEfficiency = 8f;
    public float JumpEfficiency
    {
        get { return jumpEfficiency; }
        protected set { jumpEfficiency = value; }
    }

    [SerializeField]
    private float baseMoveSpeed = 1.5f;
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
        Count
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

        equipType = EquipType.SwordAndShield;
    }

    public bool CanCarry(Item item)
    {
        if (CharacterStats.UnitCharacteristics.PlayerWeight + item.Weight <= CharacterStats.UnitCharacteristics.Weight)
            return true;

        return false;
    }

    public void RemoveEquipement(Item equip)
    {
        if (RightHand == equip)
            RightHand = null;
        else if (LeftHand == equip)
            LeftHand = null;
        else if (Helmet == equip)
            Helmet = null;
        else if (Torso == equip)
            Torso = null;
        else if (Boots == equip)
            Boots = null;
    }

    protected virtual void EquippedItemChanged()
    {
        // TODO: implemement event when equipped items changed
        //if (OnChangedWeapons != null)
        //  OnChangedWeapons.Invoke(inventory.);
    }

    protected virtual void TakeDamages(float damages)
    {
        characterStats.UnitCharacteristics.Health -= damages;
    }

    public virtual void OnHit(ACharacter character)
    {
        if (character == this)
            return;
        TakeDamages(character.CharacterStats.UnitCharacteristics.Attack - characterStats.UnitCharacteristics.Defense);
    }
}
