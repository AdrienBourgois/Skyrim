using System.Collections.Generic;

public class ItemLvlComparer : IComparer<Item>
{
    public int Compare(Item _item1, Item _item2)
    {
        if (_item1.Level > _item2.Level)
            return 1;
        if (_item1.Level == _item2.Level)
            return 0;
        return -1;
    }
}
