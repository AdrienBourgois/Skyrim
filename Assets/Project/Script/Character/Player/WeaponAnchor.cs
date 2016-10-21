using UnityEngine;
using System.Collections;
using System;

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
        Debug.Log("LOLOLol");
    }

    public void Switch()
    {
        if (weapon == null)
            return;
        Debug.Log("LALALAl");

        weaponInstance.gameObject.SetActive(!weaponInstance.gameObject.activeSelf);        
    }
}
