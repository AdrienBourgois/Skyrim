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
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}