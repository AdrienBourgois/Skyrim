using UnityEngine;
using System.Collections;

public class HealSpell : ASpell
{
    public override void Activate()
    {
        // TODO: heal selfCharacter
        Debug.Log("HEAL");
        //Destroy(gameObject);
    }
}
