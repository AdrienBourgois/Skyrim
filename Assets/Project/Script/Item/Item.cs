using System;

public class Item : IComparable<Item>
{
    protected enum item_type
    {
        undefined,
        weapon,
        armor,
        useable
    }

    public enum item_rarity
    {
        common = 1,
        uncommon = 2,
        rare = 3,
        epic = 4,
        legendary = 5
    }

    private string name_object = "Unnamed";
    public string NameObject
    {
        get { return name_object; }
        set { name_object = value; }
    }

    private string description = "Any description";
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    private float weight;
    public float Weight
    {
        get { return weight; }
        protected set { weight = value; }
    }

    private item_type type = item_type.undefined;
    protected item_type Type
    {
        get { return type; }
        set { type = value; }
    }

    private item_rarity rarity;
    public item_rarity Rarity
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

    public bool Equipped { get; set; }

    private string prefabPath = "";
    public string PrefabPath
    {
        get { return prefabPath; }
        protected set { prefabPath = value; }
    }

    protected float RangeOfGeneration = 10;

    public string GetItemGeneralInformations()
    {
        return "Name : " + name_object + " (LVL " + level + ")" +
            "\nDescription : " + description +
            "\nWeight : " + weight +
            "\nRarity : " + rarity +
            "\nPrice : " + price;
    }

    public int CompareTo(Item item)
    {
        return (NameObject.CompareTo(item.NameObject));
    }
}
