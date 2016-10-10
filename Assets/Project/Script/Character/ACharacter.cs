using UnityEngine;
using System.Collections;
using System;

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
    private float jumpEfficiency = 4.2f;

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
    [SerializeField]
    private int baseMoveSpeed;
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

    private Rigidbody rb = null;
    private bool bIsGrounded = true;
    public bool IsGrounded
    {
        get { return bIsGrounded; }
        protected set { bIsGrounded = value; }
    }

    protected virtual void Start()
    {
        /*
        characteristics.Init(baseAttack, baseDefense,
                             baseWeight, baseHealth,
                             baseMana, baseSpellPower,
                             basePrecision, baseAttackSpeed);
                             */

        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("ACharacter.Start() - could not get component of type Rigidbody");
    }

    protected abstract void Update();

    #region Controller
    public virtual void Look(Vector2 axis)
    {
        throw new NotImplementedException();
    }

    public virtual void Look(float xAxis, float yAxis)
    {
        throw new NotImplementedException();
    }
    
    public virtual void Move(float xAxis, float zAxis)
    {
        // TODO: add speed
        transform.position += new Vector3(xAxis, 0f, zAxis).normalized * Time.deltaTime;
    }

    public virtual void Jump(float xAxis = 0f, float zAxis = 0f)
    {
        Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized + Vector3.up;
        rb.AddForce(direction * jumpEfficiency, ForceMode.Impulse);
    }

    public virtual void Use()
    {
        throw new NotImplementedException();
    }

    public virtual void LeftHand()
    {
        throw new NotImplementedException();
    }

    public virtual void RightHand()
    {
        throw new NotImplementedException();
    }

    public virtual void TwoHands()
    {
        throw new NotImplementedException();
    }

    public virtual void Crouch()
    {
        throw new NotImplementedException();
    }

    public virtual void SelectMagic(int magicId)
    {
        throw new NotImplementedException();
    }

    public virtual void CastSpell()
    {
        throw new NotImplementedException();
    }
    #endregion

    protected virtual void OnTriggerEnter()
    {
        bIsGrounded = true;
    }

    protected virtual void OnTriggerExit()
    {
        bIsGrounded = false;
    }
}
