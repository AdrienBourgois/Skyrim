using UnityEngine;

public class WeaponAnchor : MonoBehaviour
{
    private ACharacter character = null;
    private Item weapon = null;
    private WeaponInstance weaponInstance = null;

    public void SetCharacter(ACharacter _character)
    {
        character = _character;
    }

    public void SetWeapon(Item _weapon)
    {
        if (weaponInstance != null)
            Destroy(weaponInstance.gameObject);
                            
        weapon = _weapon;
        weaponInstance = ItemManager.Instance.InstantiateItem(weapon);
        weaponInstance.SetCharacter(character);
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
