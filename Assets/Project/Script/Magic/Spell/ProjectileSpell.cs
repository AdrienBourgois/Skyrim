using UnityEngine;

public class ProjectileSpell : ASpell
{
    private bool cast;
    private Vector3 direction = Vector3.zero;

    public override void Activate()
    {
        Debug.Log("FIRE");
        cast = true;
        transform.parent = null;
        direction = selfController.GetTarget().transform.forward;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (cast)
            transform.position += direction * projectileSpeed * Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider _collider)
    {
        ACharacter character = _collider.gameObject.GetComponent<ACharacter>();

        if (character != null)
        {
            //if (character == selfController.Character)
            //    return;
            //// TODO: damages
            Debug.Log("DAMAGES");
        }
        Destroy(gameObject);
    }
}
