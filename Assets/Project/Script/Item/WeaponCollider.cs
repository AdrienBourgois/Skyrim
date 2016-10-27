using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private WeaponInstance weaponRoot;

    private void Start()
    {
        if (weaponRoot == null)
            Debug.LogError("WeaponCollider.Start() - weaponRoot (WeaponInstance) should not be null!");
    }

    private void OnTriggerEnter(Collider _collider)
    {
        IHitable hitableObject = _collider.transform.root.gameObject.GetComponent<IHitable>();
        if (hitableObject != null)
        {
            weaponRoot.HitObject(hitableObject);
        }
    }
}