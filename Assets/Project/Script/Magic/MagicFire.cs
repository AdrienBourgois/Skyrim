using UnityEngine;
using System.Collections;

public class MagicFire : AMagic
{
    public override void Activate()
    {
        Debug.Log("FIRE");
    }

    protected override void OnDestroy()
    {
        // TODO: Make Explosion ?
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        ACharacter character = collider.gameObject.GetComponent<ACharacter>();

        if (character != null)
        {
            if (character == selfCharacter)
                return;
            // TODO: damages
            Debug.Log("DAMAGES");
        }
        Destroy(gameObject);
    }
}
