﻿using UnityEngine;
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

    Coroutine corGrounded = null;

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
    public ACharacter Character { get { return character; } }

    [SerializeField]
    protected ACharacterWeapons characterWeapons = null;
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

        if (characterWeapons == null)
            Debug.LogError("ACharacterController.Awake() - ACharacterWeapons should not be null!");
    }
    
    protected virtual void Start()
    {
        characterWeapons.SetController(this);
    }

    protected abstract void Update();

    /// <summary>
    /// Function called to reset every trigger that may be activated in linked Animator.
    /// </summary>
    protected void ResetTriggers()
    {
        animator.ResetTrigger("TriggerJump");
        animator.ResetTrigger("TriggerSpell");
        animator.ResetTrigger("TriggerRightHand");
        animator.ResetTrigger("TriggerLeftHand");
        animator.ResetTrigger("TriggerDeath");
    }
    
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

    public virtual void ControllerLeftHand(bool bIsPressed = true)
    {
        if (animator.GetBool("IsUsingSwordAndShield"))
            animator.SetBool("IsBlocking", bIsPressed);
        else
            animator.SetTrigger("TriggerLeftHand");
    }

    public virtual void ControllerRightHand()
    {
        animator.SetTrigger("TriggerRightHand");
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
        if (animator.GetBool("IsUsing" + character.StuffType.ToString())
            || !Enum.IsDefined(typeof(MagicManager.MagicID), magicId))
            return;
        // TODO: select magic in Character and set SpellType from magicType
        characterWeapons.SetActiveMagic( (MagicManager.MagicID)magicId );
        animator.SetInteger("SpellType", magicId);
        animator.SetBool("IsUsingMagic", true);
    }

    public virtual void ControllerUnselectMagic()
    {
        characterWeapons.SetActiveMagic(MagicManager.MagicID.NONE);
        animator.SetInteger("SpellType", 0);
        animator.SetBool("IsUsingMagic", false);
    }

    public virtual void ControllerCastSpell()
    {
        animator.SetTrigger("TriggerSpell");
        if (!animator.GetBool("IsUsing" + character.StuffType.ToString()))
            characterWeapons.InstanciateMagic();
    }

    public virtual void ControllerDrawSheathSword()
    {
        string animBoolName = "IsUsing" + character.StuffType.ToString();
        animator.SetBool(animBoolName, !animator.GetBool(animBoolName));
    }
    #endregion

    public virtual void MagicActivation()
    {
        characterWeapons.ActivateMagic();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
            return;
        if (corGrounded != null)
        {
            print("OnStopCoroutine");
            StopCoroutine(corGrounded);
            corGrounded = null;
        }
        bIsGrounded = true;
        animator.SetBool("IsGrounded", true);
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
       corGrounded = StartCoroutine(CoroutineGrounded());
    }

    IEnumerator CoroutineGrounded()
    {
        yield return new WaitForSeconds(3f);
        bIsGrounded = false;
        animator.SetBool("IsGrounded", false);
    }
}
