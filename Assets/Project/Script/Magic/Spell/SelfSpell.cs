using UnityEngine;
using System.Collections;

public class SelfSpell : ASpell
{
    public override void Activate()
    {
        // TODO: heal selfCharacter
        Debug.Log("HEAL");
        Destroy(gameObject);
    }
}
