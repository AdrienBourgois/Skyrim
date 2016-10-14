using UnityEngine;
using System.Collections;

public class Player : ACharacter
{

    protected override void Update()
    {
        //if (GameManager.Instance.CurrGameState == GameManager.GameState.Pause)
        //    return;

        CharacterStats.UnitCharacteristics.Health -= 1f;

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