using System.Collections.Generic;

public class ItemNameComparer : IComparer<Item>
{
    public int Compare(Item item1, Item item2)
    {
        return (item1.NameObject.CompareTo(item2.NameObject));
    }
}
