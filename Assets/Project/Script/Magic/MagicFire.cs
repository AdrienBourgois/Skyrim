using UnityEngine;

public class MagicFire : AMagic
{
    public override void Activate()
    {
        transform.parent = null;
        Debug.Log("FIRE");
        // TODO: launch using line of sight from selfController
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
            if (character == selfController.Character)
                return;
            // TODO: damages
            Debug.Log("DAMAGES");
        }
        Destroy(gameObject);
    }
}
