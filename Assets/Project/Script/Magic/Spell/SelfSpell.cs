using UnityEngine;
using System.Collections;

public class SelfSpell : ASpell
{
    public override void Activate()
    {
        // TODO: heal selfCharacter
        Debug.Log("HEAL");

        selfController.Character.CharacterStats.BaseCharacteristics.Health += spellProperty.Power;

        Destroy(gameObject);
    }
}
