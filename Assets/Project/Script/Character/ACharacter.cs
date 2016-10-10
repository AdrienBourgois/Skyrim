using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
public abstract class ACharacter : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private string unitName;
    public string UnitName
    {
        get { return unitName; }
        protected set { unitName = value; }
    }

    [SerializeField]
    private int baseAttack;
    [SerializeField]
    private int baseDefense;
    [SerializeField]
    private int baseWeight;
    [SerializeField]
    private int baseHealth;
    [SerializeField]
    private int baseMana;
    [SerializeField]
    private int baseSpellPower;
    [SerializeField]
    private int basePrecision;
    [SerializeField]
    private int baseAttackSpeed;
    #endregion

    /* #region Stats & Inventory
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

    private Inventory inventory;
    public Inventory UnitInventory
    {
        get { return inventory; }
    }
    #endregion
    */

    protected virtual void Start()
    {
        /*
        characteristics.Init(baseAttack, baseDefense,
                             baseWeight, baseHealth,
                             baseMana, baseSpellPower,
                             basePrecision, baseAttackSpeed);
                             */
    }
}
