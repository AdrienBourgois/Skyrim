using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    List<Item> inventory = new List<Item>();

    public void AddItem<T>(T item) where T : Item
    {
        inventory.Add(item);
    }

    public void DisplayInventory()
    {
        foreach (Item item in inventory)
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
        foreach (Item item in inventory)
        {
            if (item is T)
                items_list.Add(item);
        }

        return items_list;
    }

    public List<Item> GetItemsByType<T>() where T : Item
    {
        List<Item> items_list = new List<Item>();
        foreach (Item item in inventory)
        {
            if (item is T)
                items_list.Add(item);
        }

        return items_list;
    }

    public void Awake()
    {
        ItemManager im = new ItemManager();
        inventory = im.GenerateInventory(ItemManager.flags_generation.All_Type, 100);
    }

}
