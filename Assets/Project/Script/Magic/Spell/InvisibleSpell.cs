using UnityEngine;

public class InvisibleSpell : ASpell
{

    public override void Activate()
    {
        Debug.Log("Invisibility");

        Destroy(gameObject);
    }
}
