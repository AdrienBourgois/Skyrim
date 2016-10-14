using UnityEngine;
using System.Collections;

public class PlayerController : ACharacterController
{
    protected override void Update()
    {
        ResetTriggers();
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
            ControllerLeftHand(true);
        else if (Input.GetButtonUp("LeftHand"))
            ControllerLeftHand(false);

        if (Input.GetButtonDown("SwitchWeapon"))
            ControllerDrawSheathSword();
        #endregion

        if (Input.GetButtonDown("Use"))
            ControllerUse();
    }
}