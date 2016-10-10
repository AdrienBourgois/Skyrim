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
                Jump(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            else
                Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}