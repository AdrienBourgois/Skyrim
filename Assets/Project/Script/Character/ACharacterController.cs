using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Abstract for every Character in the game. The Controller permit to do actions and animations using ACharacter's stats.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class ACharacterController : APausableObject
{
    #region Serialized Fields
    [SerializeField]
    private Rigidbody rb;
    public Rigidbody RBody { get { return rb; } }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    protected ACharacter character;
    public ACharacter Character { get { return character; } }

    [SerializeField]
    protected ACharacterWeapons characterWeapons;

    [SerializeField]
    private GameObject centerOfMass;
    public Transform CenterOfMass { get { return centerOfMass.transform; } }
    #endregion

    private Coroutine corGrounded;

    private bool bIsGrounded = true;
    protected bool IsGrounded
    {
        get { return bIsGrounded; }
        set { bIsGrounded = value; }
    }

    protected void Awake()
    {
        GameManager.OnPause += PutPause;

        if (rb == null)
            Debug.LogError("ACharacterController.Awake() - Rigidbody should not be null!");

        if (animator == null)
            Debug.LogError("ACharacterController.Awake() - Animator should not be null!");

        if (character == null)
            Debug.LogError("ACharacterController.Awake() - Character should not be null!");

        if (characterWeapons == null)
            Debug.LogError("ACharacterController.Awake() - CharacterWeapons should not be null!");

        if (centerOfMass == null)
            Debug.LogError("ACharacterController.Awake() - Center of Mass should not be null!");
    }

    protected virtual void Start()
    {
        characterWeapons.SetController(this);
        characterWeapons.SetCharacter(character);
    }

    protected override void PutPause()
    {
        base.PutPause();
        ControllerMove(0f, 0f);
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
    public virtual void ControllerLook(Vector2 _axis)
    {
        transform.localEulerAngles = new Vector3(_axis.x, _axis.y, 0f);
    }

    public virtual void ControllerLook(float _xAxis, float _yAxis)
    {
        float lookY = _yAxis - transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0f, _yAxis, 0f);
        animator.SetFloat("LookY", lookY);
    }

    protected virtual void ControllerMove(float _xAxis, float _zAxis)
    {
        animator.SetFloat("MoveSpeed", character.MoveSpeed);
        animator.SetFloat("MoveX", _xAxis, 0.4f, Time.deltaTime);
        animator.SetFloat("MoveZ", _zAxis, 0.4f, Time.deltaTime);
    }

    protected virtual void ControllerJump(float _xAxis = 0f, float _zAxis = 0f)
    {
        animator.SetFloat("MoveX", _xAxis, 0.4f, Time.deltaTime);
        animator.SetFloat("MoveZ", _zAxis, 0.4f, Time.deltaTime);
        animator.SetTrigger("TriggerJump");
        animator.SetFloat("JumpEfficiency", character.JumpEfficiency);
    }

    public virtual void ControllerUse()
    {
        RaycastHit hit;
        
        const float useMaxDistance = 2f;
        if (Physics.Raycast(transform.position, transform.forward, out hit, useMaxDistance, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            IUsableObject usableCollider = hit.collider.GetComponent<IUsableObject>();

            if (usableCollider != null)
            {
                usableCollider.OnUse(character);
            }
        }
    }

    protected virtual void ControllerLeftHand(bool _bIsPressed = true)
    {
        if (animator.GetBool("IsUsingSwordAndShield"))
            animator.SetBool("IsBlocking", _bIsPressed);
        else
            animator.SetTrigger("TriggerLeftHand");
    }

    protected virtual void ControllerRightHand()
    {
        animator.SetTrigger("TriggerRightHand");
    }

    public virtual void ControllerTwoHands()
    {
        throw new NotImplementedException();
    }

    protected virtual void ControllerCrouch(bool _bIsCrouch)
    {
        animator.SetBool("IsCrouching", _bIsCrouch);
    }

    protected virtual void ControllerSelectMagic(int _key)
    {
        if (MagicManager.Instance.MagicKeySelected[_key] == null)
            return;

        SpellProperty selectedMagic = MagicManager.Instance.MagicKeySelected[_key];

        if (animator.GetBool("IsUsing" + character.StuffType)
            || !Enum.IsDefined(typeof(MagicManager.MagicId), selectedMagic.Id))
            return;

        characterWeapons.SetActiveMagic(selectedMagic);
        animator.SetInteger("SpellType", (int)selectedMagic.Type);
        animator.SetBool("IsUsingMagic", true);
    }

    protected virtual void ControllerUnselectMagic()
    {
        characterWeapons.SetActiveMagic(null);
        animator.SetInteger("SpellType", 0);
        animator.SetBool("IsUsingMagic", false);
    }

    protected virtual void ControllerCastSpell()
    {
        if (!animator.GetBool("IsUsing" + character.StuffType))
            if (characterWeapons.InstanciateMagic())
                animator.SetTrigger("TriggerSpell");
    }

    protected virtual void ControllerDrawSheathSword()
    {
        string animBoolName = "IsUsing" + character.StuffType;
        animator.SetBool(animBoolName, !animator.GetBool(animBoolName));
    }
    #endregion

    public void MagicActivation()
    {
        characterWeapons.ActivateMagic();
    }

    protected virtual void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.layer == LayerMask.NameToLayer("Character"))
            return;
        if (corGrounded != null)
        {
            StopCoroutine(corGrounded);
            corGrounded = null;
        }
        bIsGrounded = true;
        animator.SetBool("IsGrounded", true);
    }

    protected virtual void OnTriggerStay(Collider _collider)
    {
        if (_collider.gameObject.layer == LayerMask.NameToLayer("Character"))
            return;
        if (bIsGrounded == false)
        {
            bIsGrounded = true;
            animator.SetBool("IsGrounded", true);
        }
        if (corGrounded != null)
        {
            StopCoroutine(corGrounded);
            corGrounded = null;
        }
    }

    protected virtual void OnTriggerExit(Collider _collider)
    {
        if (corGrounded == null)
            corGrounded = StartCoroutine(CoroutineGrounded());
    }

    private IEnumerator CoroutineGrounded()
    {
        yield return new WaitForSeconds(0.3f);
        bIsGrounded = false;
        animator.SetBool("IsGrounded", false);
    }

    public abstract Transform GetTarget();
}
