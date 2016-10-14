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

        if (Input.GetButtonDown("Use"))
            ControllerUse();

        ControllerCrouch(Input.GetButton("Crouch"));
    }
}