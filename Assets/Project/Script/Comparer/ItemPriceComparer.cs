using System.Collections.Generic;

public class ItemPriceComparer : IComparer<Item>
{
    public int Compare(Item item1, Item item2)
    {
        if (item1.Price > item2.Price)
            return 1;
        if (item1.Price == item2.Price)
            return 0;
        return -1;
    }
}
