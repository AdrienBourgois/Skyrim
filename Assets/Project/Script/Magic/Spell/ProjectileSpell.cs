using UnityEngine;

public class ProjectileSpell : ASpell
{
    [SerializeField]
    protected float projectileSpeed = 10f;
    
    private bool cast;
    private Vector3 direction = Vector3.zero;

    public override void Activate()
    {
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
        IHitable hitableObject = _collider.transform.root.gameObject.GetComponent<IHitable>();
        if (hitableObject as ACharacter == selfController.Character)
            return;
        if (hitableObject != null)
        {
            hitableObject.OnHit(selfController.Character, spellProperty.Power);
        }
        Destroy(gameObject);
    }
}
