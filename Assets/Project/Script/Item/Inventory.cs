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
            if(item is IInstanciableItem)
            {
                IInstanciableItem instanciable_item = (IInstanciableItem)item;
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
            if (item is IInstanciableItem)
            {
                IInstanciableItem instanciable_item = (IInstanciableItem)item;
                Debug.Log(instanciable_item.GetItemInformations());
            }
            else
                Debug.Log(item.GetItemGeneralInformations());
        }
    }

    public void Start()
    {
        ItemManager im = gameObject.AddComponent<ItemManager>();
        AddItem(im.CreateObject<Weapon>(Item.item_type.weapon, Item.item_rarity.epic, "Sword", "Simple Sword"));
        //DisplayInventory();
        DisplayList(GetItems<Weapon>());
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

    public List<T> GetItemsByType<T>() where T : Item
    {
        List<T> items_list = new List<T>();
        foreach (Item item in inventory)
        {
            if (item is T)
                items_list.Add((T)item);
        }

        return items_list;
    }
}
