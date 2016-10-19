public class Weapon : Item, ITypeItem {

    public enum weapon_type
    {
        Sword,
        Axe
    }
    public weapon_type WeaponType;

    private float damage_value;
    public float Damage
    {
        get { return damage_value; }
        set { damage_value = value; }
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n=====================================" +
            "\nDamage : " + Damage;
    }
}
