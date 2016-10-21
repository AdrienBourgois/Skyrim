using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    private List<Item> list = new List<Item>();
    public List<Item> List
    {
        get { return list; }
        set { list = value; }
    }

    public void AddItem<T>(T item) where T : Item
    {
        Debug.Log(item);
        list.Add(item);
    }

    public void RemoveItem(Item item)
    {
        list.Remove(item);
    }

    public void DisplayInventory()
    {
        foreach (Item item in list)
        {
            if (item is ITypeItem)
            {
                ITypeItem instanciable_item = (ITypeItem)item;
                Debug.Log(instanciable_item.GetItemInformations());
            }
            else
                Debug.Log(item.GetItemGeneralInformations());
        }
    }

    public void DisplayList<T>(List<T> list) where T : Item
    {
        foreach (T item in list)
        {
            if (item is ITypeItem)
            {
                ITypeItem instanciable_item = (ITypeItem)item;
                Debug.Log(instanciable_item.GetItemInformations());
            }
            else
                Debug.Log(item.GetItemGeneralInformations());
        }
    }

    public List<Item> GetItems<T>() where T : Item
    {
        List<Item> items_list = new List<Item>();
        foreach (Item item in list)
        {
            if (item is T)
                items_list.Add(item);
        }

        return items_list;
    }

    public List<Item> GetItemsByType<T>() where T : Item
    {
        List<Item> items_list = new List<Item>();
        foreach (Item item in list)
        {
            if (item is T)
                items_list.Add(item);
        }

        return items_list;
    }

    public List<Item> GetItemsByTypeSorted<T>(string sort_type)
    {
        List<Item> items_list = new List<Item>();
        foreach (Item item in list)
        {
            if (item is T)
                items_list.Add(item);
        }

        if (sort_type == "LVL")
            items_list.Sort(new ItemLVLComparer());
        else if (sort_type == "Name")
            items_list.Sort(new ItemNameComparer());
        else if (sort_type == "Weight")
            items_list.Sort(new ItemWeightComparer());
        else if (sort_type == "Price")
            items_list.Sort(new ItemPriceComparer());

        return items_list;
    }
}
