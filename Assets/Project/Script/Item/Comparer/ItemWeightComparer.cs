using System.Collections.Generic;

public class ItemWeightComparer : IComparer<Item>
{
    public int Compare(Item _item1, Item _item2)
    {
        if (_item1.Weight > _item2.Weight)
            return 1;
        if (_item1.Weight == _item2.Weight)
            return 0;
        return -1;
    }
}
