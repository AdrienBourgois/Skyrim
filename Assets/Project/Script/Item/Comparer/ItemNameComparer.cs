using System.Collections.Generic;

public class ItemNameComparer : IComparer<Item>
{
    public int Compare(Item _item1, Item _item2)
    {
        return (_item1.NameObject.CompareTo(_item2.NameObject));
    }
}
