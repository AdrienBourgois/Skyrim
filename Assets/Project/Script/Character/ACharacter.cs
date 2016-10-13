using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Abstract class for every character in the game. An ACharacter has a UnitName and Base Stats as serialized fields.
/// </summary>
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
[RequireComponent (typeof(CapsuleCollider))]
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

    [SerializeField]
    private CapsuleCollider capTrig = null;
    public CapsuleCollider CapsuleTrigger
    {
        get { return capTrig; }
    }

    [SerializeField]
    private CapsuleCollider capCol = null;
    public CapsuleCollider CapsuleCollider
    {
        get { return capCol; }
    }
    #endregion

    private Rigidbody rb = null;
    protected Animator animator = null;

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

        animator = GetComponent<Animator>();

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
        transform.localEulerAngles = new Vector3(0f, yAxis, 0f);
    }
    
    public virtual void ControllerMove(float xAxis, float zAxis)
    {
        animator.SetFloat("MoveX", xAxis, baseMoveSpeed / 6, Time.deltaTime);
        animator.SetFloat("MoveZ", zAxis, baseMoveSpeed / 6, Time.deltaTime);
    }

    public virtual void ControllerJump(float xAxis = 0f, float zAxis = 0f)
    {
        animator.SetFloat("MoveX", xAxis, baseMoveSpeed / 6, Time.deltaTime);
        animator.SetFloat("MoveZ", zAxis, baseMoveSpeed / 6, Time.deltaTime);
        animator.SetTrigger("TriggerJump");
        Vector3 direction = transform.forward * zAxis + transform.right * xAxis;
        direction.Normalize();
        direction.y = 1;
        rb.AddForce(transform.up * jumpEfficiency, ForceMode.Impulse);
        animator.SetFloat("JumpEfficiency", jumpEfficiency);
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

    public virtual void ControllerCrouch(bool bIsCrouch)
    {
        animator.SetBool("IsCrouching", bIsCrouch);
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
        animator.SetBool("IsGrounded", true);
    }

    protected virtual void OnTriggerExit()
    {
        bIsGrounded = false;
        animator.SetBool("IsGrounded", false);
    }
}
