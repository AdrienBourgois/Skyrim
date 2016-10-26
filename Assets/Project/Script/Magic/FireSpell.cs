using UnityEngine;
using System.Collections;

public class FireSpell : ASpell
{
    public override void Activate()
    {
     //   gao = new GameObject();
        Debug.Log("FIRE");
        // Destroy (gao, lifetime)
        // TODO: launch using line of sight from selfController
    }

    //protected override void OnDestroy()
    //{
    //    // TODO: Make Explosion ?
    //}

    protected virtual void OnTriggerEnter(Collider collider)
    {
        ACharacter character = collider.gameObject.GetComponent<ACharacter>();

        if (character != null)
        {
            //if (character == selfController.Character)
            //    return;
            //// TODO: damages
            Debug.Log("DAMAGES");
        }
        //Destroy(gao);
    }
}
