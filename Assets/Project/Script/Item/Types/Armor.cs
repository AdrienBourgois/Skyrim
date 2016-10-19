public class Armor : Item, ITypeItem {

    public enum armor_type
    {
        Helmet,
        Torso,
        Boots,
        Shield
    }
    public armor_type ArmorType;

    private float defense_value;
    public float Defense
    {
        get { return defense_value; }
        set { defense_value = value; }
    }

    public string GetItemInformations()
    {
        return GetItemGeneralInformations() +
            "\n=====================================" +
            "\nArmor : " + defense_value;
    }
}
