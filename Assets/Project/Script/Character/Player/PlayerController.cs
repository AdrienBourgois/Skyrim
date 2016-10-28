using UnityEngine;

public class PlayerController : ACharacterController
{
    Transform cameraTransform = null;

    protected override void Awake()
    {
        base.Awake();

        // HACK: doesnt work everytime in ACharacter start.. not normal - maybe will be the same in Enemy if being Instantiated
        if (characterWeapons != null)
            characterWeapons.SetCharacter(character);
    }

    protected override void Start()
    {
        base.Start();
        cameraTransform = FindObjectOfType<Cam>().transform;
    }

    protected override void Update()
    {
        ResetTriggers();

        if (cameraTransform == null)
            cameraTransform = FindObjectOfType<Cam>().transform;

        if (paused)
        {
            ControllerMove(0f, 0f);
            return;
        }

        UpdateInput();
    }

    private void UpdateInput()
    {
        #region Movement / Locomotion
        if (IsGrounded)
        {
            if (Input.GetButtonDown("Jump"))
                ControllerJump(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            else
                ControllerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        ControllerCrouch(Input.GetButton("Crouch"));
        #endregion

        #region Magic
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetButtonDown("Magic " + i))
            {
                ControllerSelectMagic(i);
                break;
            }
        }

        if (Input.GetButtonDown("No Magic"))
            ControllerUnselectMagic();

        if (Input.GetButtonDown("CastSpell"))
            ControllerCastSpell();
        #endregion

        #region Hands actions
        if (Input.GetButtonDown("RightHand"))
            ControllerRightHand();

        if (Input.GetButtonDown("LeftHand"))
            ControllerLeftHand();
        else if (Input.GetButtonUp("LeftHand"))
            ControllerLeftHand(false);

        if (Input.GetButtonDown("SwitchWeapon"))
            ControllerDrawSheathSword();
        #endregion

        if (Input.GetButtonDown("Use"))
            ControllerUse();
    }

    public override void ControllerUse()
    {
        RaycastHit hit;
        // TODO: global(?) variable for max distance
        const float useMaxDistance = 3f;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, useMaxDistance, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            IUsableObject usableCollider = hit.collider.GetComponent<IUsableObject>();

            if (usableCollider != null)
            {
                usableCollider.OnUse(character);
            }
        }
    }

    public override Transform GetTarget()
    {
        return cameraTransform;
    }
}