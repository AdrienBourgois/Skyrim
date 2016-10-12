using UnityEngine;
using System.Collections;

public class Player : ACharacter
{

    protected override void Start()
    {
        base.Start();

        rb.freezeRotation = true;
    }

    protected override void Update()
    {
        CharacterStats.UnitCharacteristics.Health -= 1;
        CharacterStats.UnitCharacteristics.Mana -= 1;

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
    }
}