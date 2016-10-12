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

    public void Start()
    {
        ItemManager im = gameObject.AddComponent<ItemManager>();
        //AddItem(im.CreateObject<Weapon>(Item.item_rarity.epic, "Epic Sword", "Simple Sword"));
        //AddItem(im.CreateObject<Weapon>(Item.item_rarity.rare, "Rare Sword", "Simple Sword"));
        //AddItem(im.CreateObject<Weapon>(Item.item_rarity.common, "Common Sword", "Simple Sword"));
        //AddItem(im.CreateObject<Armor>(Item.item_rarity.legendary, "Lengendary Helmet", "Simple Helmet"));
        //AddItem(im.CreateObject<Armor>(Item.item_rarity.uncommon, "Uncommon Helmet", "Simple Helmet"));
        for (int i = 0; i < 10; i++)
            AddItem(im.CreateObject<Armor>());
        for (int i = 0; i < 10; i++)
            AddItem(im.CreateObject<Weapon>());
        DisplayInventory();
    }

}
