using System;

public class Item : IComparable<Item>
{
    protected enum ItemType
    {
        Undefined,
        Weapon,
        Armor,
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
        get { return description; }
        set { description = value; }
    }

    private float weight = 0;
    public float Weight
    {
        get { return weight; }
        protected set { weight = value; }
    }

    private ItemType type = ItemType.Undefined;
    protected ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    private ItemRarity rarity;
    public ItemRarity Rarity
    {
        protected get { return rarity; }
        set { rarity = value; }
    }

    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    private int price;
    public int Price
    {
        get { return price; }
        protected set { price = value; }
    }

    public ACharacter Equipped { get; set; }

    private string prefabPath = "";
    public string PrefabPath
    {
        get { return prefabPath; }
        protected set { prefabPath = value; }
    }

    protected float rangeOfGeneration = 10;

    public string GetItemGeneralInformations()
    {
        return "Name : " + nameObject + " (LVL " + level + ")" +
            "\nDescription : " + description +
            "\nWeight : " + weight +
            "\nRarity : " + rarity +
            "\nPrice : " + price;
    }

    public int CompareTo(Item _item)
    {
        return (NameObject.CompareTo(_item.NameObject));
    }
}
