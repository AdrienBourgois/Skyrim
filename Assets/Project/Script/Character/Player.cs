using UnityEngine;
using System.Collections;

public class Player : ACharacter
{

    protected override void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (IsGrounded)
        {
            if (Input.GetButtonDown("Jump"))
                ControllerJump(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            else
                ControllerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        if (Input.GetButtonDown("Use"))
            ControllerUse();

        ControllerCrouch(Input.GetButton("Crouch"));
    }

    public override void ControllerUse()
    {
        base.ControllerUse();
        animator.SetBool("IsUsingMagic", true);
    }
}