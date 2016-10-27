using UnityEngine;
using System.Collections;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField]
    WeaponInstance weaponRoot = null;

    private void Start()
    {
        if (weaponRoot == null)
            Debug.LogError("WeaponCollider.Start() - weaponRoot (WeaponInstance) should not be null!");
    }

    private void OnTriggerEnter(Collider collider)
    {
        IHitable hitableObject = collider.transform.root.gameObject.GetComponent<IHitable>();
        if (hitableObject != null)
        {
            weaponRoot.HitObject(hitableObject);
        }
    }
}