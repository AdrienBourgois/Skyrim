using System.Collections.Generic;

public class ItemWeightComparer : IComparer<Item>
{
    public int Compare(Item item1, Item item2)
    {
        if (item1.Weight > item2.Weight)
            return 1;
        if (item1.Weight == item2.Weight)
            return 0;
        return -1;
    }
}
