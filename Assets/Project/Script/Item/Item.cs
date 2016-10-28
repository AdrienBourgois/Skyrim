using System;

public class Item : IComparable<Item>
{
    protected enum ItemType
    {
        Undefined,
        Weapon,
        Armor,
        [Useless]
        Useable
    }

    public enum ItemRarity
    {
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5
    }

    private string nameObject = "Unnamed";
    public string NameObject
    {
        get { return nameObject; }
        set { nameObject = value; }
    }

    private string description = "Any description";
    public string Description
    {
        [Useless]
        get { return description; }
        set { description = value; }
    }

    public float Weight { get; protected set; }

    private ItemType type = ItemType.Undefined;
    protected ItemType Type
    {
        [Useless]
        get { return type; }
        set { type = value; }
    }

    public ItemRarity Rarity { protected get; set; }

    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Price { get; protected set; }

    public ACharacter Equipped { get; protected set; }

    private string prefabPath = "";
    public string PrefabPath
    {
        get { return prefabPath; }
        protected set { prefabPath = value; }
    }

    protected const float RangeOfGeneration = 10;

    public string GetItemGeneralInformations()
    {
        return "Name : " + nameObject + " (LVL " + level + ")" +
            "\nDescription : " + description +
            "\nWeight : " + Weight +
            "\nRarity : " + Rarity +
            "\nPrice : " + Price;
    }

    public int CompareTo(Item _item)
    {
        return NameObject.CompareTo(_item.NameObject);
    }
}
