using System.Collections.Generic;

public class ItemPriceComparer : IComparer<Item>
{
    public int Compare(Item _item1, Item _item2)
    {
        if (_item1.Price > _item2.Price)
            return 1;
        if (_item1.Price == _item2.Price)
            return 0;
        return -1;
    }
}
