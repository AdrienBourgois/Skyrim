using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Abstract for every Character in the game. The Controller permit to do actions and animations using ACharacter's stats.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public abstract class ACharacterController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private CapsuleCollider capCol = null;
    public CapsuleCollider CapsuleCollider
    {
        get { return capCol; }
    }

    [SerializeField]
    protected Rigidbody rb = null;

    [SerializeField]
    protected Animator animator = null;

    [SerializeField]
    protected ACharacter character = null;
    #endregion
    
    private bool bIsGrounded = true;
    public bool IsGrounded
    {
        get { return bIsGrounded; }
        protected set { bIsGrounded = value; }
    }

    // Use this for initialization
    protected void Awake()
    {
        if (capCol == null)
            Debug.LogError("ACharacterController.Awake() - CapsuleCollider should not be null!");

        if (rb == null)
            Debug.LogError("ACharacterController.Awake() - Rigidbody should not be null!");

        if (animator == null)
            Debug.LogError("ACharacterController.Awake() - Animator should not be null!");

        if (character == null)
            Debug.LogError("ACharacterController.Awake() - ACharacter should not be null!");
    }

    protected abstract void Update();
    
    #region Controller
    public virtual void ControllerLook(Vector2 axis)
    {
        transform.localEulerAngles = new Vector3(axis.x, axis.y, 0f);
    }

    public virtual void ControllerLook(float xAxis, float yAxis)
    {
        float lookY = yAxis - transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0f, yAxis, 0f);
        animator.SetFloat("LookY", lookY);
    }

    public virtual void ControllerMove(float xAxis, float zAxis)
    {
        animator.SetFloat("MoveX", xAxis, character.MoveSpeed / 10, Time.deltaTime);
        animator.SetFloat("MoveZ", zAxis, character.MoveSpeed / 10, Time.deltaTime);
    }

    public virtual void ControllerJump(float xAxis = 0f, float zAxis = 0f)
    {
        animator.SetFloat("MoveX", xAxis, character.MoveSpeed / 10, Time.deltaTime);
        animator.SetFloat("MoveZ", zAxis, character.MoveSpeed / 10, Time.deltaTime);
        animator.SetTrigger("TriggerJump");
        Vector3 direction = transform.forward * zAxis + transform.right * xAxis;
        direction.Normalize();
        direction.y = 1;
        rb.AddForce(transform.up * character.JumpEfficiency, ForceMode.Impulse);
        animator.SetFloat("JumpEfficiency", character.JumpEfficiency);
    }

    public virtual void ControllerUse()
    {
        RaycastHit hit;
        // TODO: global(?) variable for max distance
        float useMaxDistance = 2f;
        if (Physics.Raycast(transform.position, transform.forward, out hit, useMaxDistance, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            IUsableObject usableCollider = hit.collider.GetComponent<IUsableObject>();

            if (usableCollider != null)
            {
                usableCollider.OnUse(character);
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
        animator.SetInteger("SpellType", magicId);
        animator.SetBool("IsUsingMagic", true);
        Debug.Log("Selected magic num [" + magicId + "]");
    }

    public virtual void ControllerUnselectMagic()
    {
        animator.SetInteger("SpellType", 0);
        animator.SetBool("IsUsingMagic", false);
        Debug.Log("Unselected Magic");
    }

    public virtual void ControllerCastSpell()
    {
        animator.SetTrigger("TriggerSpell");
    }
    #endregion

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
            return;
        bIsGrounded = true;
        animator.SetBool("IsGrounded", true);
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        bIsGrounded = false;
        animator.SetBool("IsGrounded", false);
    }
}
