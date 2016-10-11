using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
[RequireComponent (typeof(Rigidbody))]
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
    private float baseWeight;
    [SerializeField]
    private int baseHealth;
    [SerializeField]
    private int baseMana;
    [SerializeField]
    private int baseSpellPower;
    [SerializeField]
    private float basePrecision;
    [SerializeField]
    private float baseAttackSpeed;
    [SerializeField]
    private float baseMoveSpeed = 3f;
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
    public virtual void ControllerLook(Vector2 axis)
    {
        transform.localEulerAngles = new Vector3(axis.x, axis.y, 0f);
    }

    public virtual void ControllerLook(float xAxis, float yAxis)
    {
        transform.localEulerAngles = new Vector3(xAxis, yAxis, 0f);
    }
    
    public virtual void ControllerMove(float xAxis, float zAxis)
    {
        Vector3 direction = transform.forward * zAxis + transform.right * xAxis;
        direction.y = 0;
        direction.Normalize();
        // TODO: add stat speed
        transform.position += direction.normalized * baseMoveSpeed * Time.deltaTime;
    }

    public virtual void ControllerJump(float xAxis = 0f, float zAxis = 0f)
    {
        Vector3 direction = transform.forward * zAxis + transform.right * xAxis;
        direction.Normalize();
        direction.y = 1;
        rb.AddForce(direction * jumpEfficiency, ForceMode.Impulse);
    }

    public virtual void ControllerUse()
    {
        RaycastHit hit;
        // TODO: global(?) variable for max distance
        float useMaxDistance = 1000f;
        if (Physics.Raycast(transform.position, transform.forward, out hit, useMaxDistance, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            IUsableObject usableCollider = hit.collider.GetComponent<IUsableObject>();

            if (usableCollider != null)
            {
                usableCollider.OnUse(this);
            }
        }
    }

    public virtual void ControllerLeftHand()
    {
        throw new NotImplementedException();
    }

    public virtual void ControllerRightHand()
    {
        throw new NotImplementedException();
    }

    public virtual void ControllerTwoHands()
    {
        throw new NotImplementedException();
    }

    public virtual void ControllerCrouch()
    {
        throw new NotImplementedException();
    }

    public virtual void ControllerSelectMagic(int magicId)
    {
        throw new NotImplementedException();
    }

    public virtual void ControllerCastSpell()
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
