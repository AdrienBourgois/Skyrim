using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private List<Item> list = new List<Item>();
    public List<Item> List
    {
        get { return list; }
        set { list = value; }
    }

    public void AddItem<T>(T _item) where T : Item
    {
        list.Add(_item);
    }

    public void RemoveItem(Item _item)
    {
        list.Remove(_item);
    }

    public void DisplayInventory()
    {
        foreach (Item item in list)
        {
            if (item is ITypeItem)
            {
                ITypeItem instanciableItem = (ITypeItem)item;
                Debug.Log(instanciableItem.GetItemInformations());
            }
            else
                Debug.Log(item.GetItemGeneralInformations());
        }
    }

    public void DisplayList<T>(List<T> _listToDisplay) where T : Item
    {
        foreach (T item in _listToDisplay)
        {
            if (item is ITypeItem)
            {
                ITypeItem instanciableItem = (ITypeItem)item;
                Debug.Log(instanciableItem.GetItemInformations());
            }
            else
                Debug.Log(item.GetItemGeneralInformations());
        }
    }

    public List<Item> GetItems<T>() where T : Item
    {
        List<Item> itemsList = new List<Item>();
        foreach (Item item in list)
        {
            if (item is T)
                itemsList.Add(item);
        }

        return itemsList;
    }

    public List<Item> GetItemsByType<T>() where T : Item
    {
        List<Item> itemsList = new List<Item>();
        foreach (Item item in list)
        {
            if (item is T)
                itemsList.Add(item);
        }

        return itemsList;
    }

    public List<Item> GetItemsByTypeSorted<T>(string _sortType)
    {
        List<Item> itemsList = new List<Item>();
        foreach (Item item in list)
        {
            if (item is T)
                itemsList.Add(item);
        }

        if (_sortType == "LVL")
            itemsList.Sort(new ItemLvlComparer());
        else if (_sortType == "Name")
            itemsList.Sort(new ItemNameComparer());
        else if (_sortType == "Weight")
            itemsList.Sort(new ItemWeightComparer());
        else if (_sortType == "Price")
            itemsList.Sort(new ItemPriceComparer());

        return itemsList;
    }
}
