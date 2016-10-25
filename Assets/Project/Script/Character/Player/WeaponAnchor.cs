using UnityEngine;

public class WeaponAnchor : MonoBehaviour
{
    private Item weapon = null;
    private WeaponInstance weaponInstance = null;

    public void SetWeapon(Item _weapon)
    {
        if (weaponInstance != null)
            Destroy(weaponInstance.gameObject);
                            
        weapon = _weapon;
        weaponInstance = ItemManager.Instance.InstantiateItem(weapon);
        weaponInstance.gameObject.transform.parent = gameObject.transform;
        weaponInstance.gameObject.transform.localPosition = Vector3.zero;
        weaponInstance.gameObject.SetActive(false);
    }

    public void Switch()
    {
        if (weapon == null)
            return;

        weaponInstance.gameObject.SetActive(!weaponInstance.gameObject.activeSelf);        
    }
}
