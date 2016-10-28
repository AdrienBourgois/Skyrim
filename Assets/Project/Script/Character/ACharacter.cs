using UnityEngine;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
public abstract class ACharacter : MonoBehaviour, IHitable
{    
    public delegate void DelegateWeapons(Item _leftWeapon, Item _rightWeapon);
    [Useless]
    public event DelegateWeapons OnChangedWeapons;

    #region Equipement

    public Weapon RightHand { get; set; }

    public Shield LeftHand { get; set; }

    public Helmet Helmet { get; set; }

    public Torso Torso { get; set; }

    public Boots Boots { get; set; }

    #endregion

    #region Serialized Fields
    [SerializeField]
    private string unitName;
    [Useless]
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
        [Useless]
        protected set { unitLevel = value; }
    }    

    [SerializeField]
    private float jumpEfficiency = 8f;
    public float JumpEfficiency
    {
        get { return jumpEfficiency; }
        [Useless]
        protected set { jumpEfficiency = value; }
    }

    [SerializeField]
    private float baseMoveSpeed = 1.5f;
    public float MoveSpeed
    {
        get { return baseMoveSpeed; }
        [Useless]
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
        [Useless]
        Axe             =  1,
        [Useless]
        Count
    }

    private EquipType equipType = EquipType.None;
    public EquipType StuffType
    {
        get { return equipType; }
        [Useless]
        protected set { equipType = value; }
    }

    protected virtual void Start()
    {
        characterStats.SetCharacteristics(this);
        CharacterStats.UnitCharacteristics.RegenFullHealthAndMana();
        CharacterStats.TriggerDeath += OnDeath;

        equipType = EquipType.SwordAndShield;
    }

    public bool CanCarry(Item _item)
    {
        if (CharacterStats.UnitCharacteristics.PlayerWeight + _item.Weight <= CharacterStats.UnitCharacteristics.Weight)
            return true;

        return false;
    }

    public void RemoveEquipement(Item _equip)
    {
        if (RightHand == _equip)
            RightHand = null;
        else if (LeftHand == _equip)
            LeftHand = null;
        else if (Helmet == _equip)
            Helmet = null;
        else if (Torso == _equip)
            Torso = null;
        else if (Boots == _equip)
            Boots = null;
    }

    [Useless]
    protected virtual void EquippedItemChanged()
    {
        // TODO: implemement event when equipped items changed
        //if (OnChangedWeapons != null)
        //  OnChangedWeapons.Invoke(inventory.);
    }

    protected virtual void TakeDamages(float _damages)
    {
        if (_damages <= 0)
            _damages = 1;
        characterStats.UnitCharacteristics.Health -= _damages;
    }

    public virtual void EarnXp(int _xpReward)
    {
        // Do nothing
    }

    public virtual void OnHit(ACharacter _character)
    {
        if (_character == this)
            return;
        TakeDamages(_character.CharacterStats.UnitCharacteristics.Attack - characterStats.UnitCharacteristics.Defense);
    }

    protected abstract void OnDeath();
}
