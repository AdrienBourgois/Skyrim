using System.Collections.Generic;

public class ItemLVLComparer : IComparer<Item>
{
    public int Compare(Item item1, Item item2)
    {
        if (item1.Level > item2.Level)
            return 1;
        if (item1.Level == item2.Level)
            return 0;
        return -1;
    }
}
