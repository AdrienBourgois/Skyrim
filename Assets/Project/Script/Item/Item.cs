using System;

public class Item : IComparable<Item>
{
    public enum item_type
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

    private float weight = 0;
    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    private item_type type = item_type.undefined;
    public item_type Type
    {
        get { return type; }
        set { type = value; }
    }

    private item_rarity rarity;
    public item_rarity Rarity
    {
        get { return rarity; }
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
        set { price = value; }
    }

    private bool is_equipped;
    public bool Equipped
    {
        get { return is_equipped; }
        set { is_equipped = value; }
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
