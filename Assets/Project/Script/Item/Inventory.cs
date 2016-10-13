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
        AddItem(im.CreateObject<Sword>(Item.item_rarity.epic, "Epic Sword", "Simple Sword"));
        AddItem(im.CreateObject<Axe>(Item.item_rarity.rare, "Rare Axe", "Simple Axe"));
        AddItem(im.CreateObject<Boots>(Item.item_rarity.common, "Common Boots", "Simple Boots"));
        AddItem(im.CreateObject<Torso>(Item.item_rarity.legendary, "Lengendary Torso", "Simple Torso"));
        AddItem(im.CreateObject<Shield>(Item.item_rarity.uncommon, "Uncommon Shield", "Simple Shield"));
        AddItem(im.CreateObject<Sword>(Item.item_rarity.epic, "Epic Sword", "Simple Sword"));
        AddItem(im.CreateObject<Axe>(Item.item_rarity.rare, "Rare Axe", "Simple Axe"));
        AddItem(im.CreateObject<Boots>(Item.item_rarity.common, "Common Boots", "Simple Boots"));
        AddItem(im.CreateObject<Torso>(Item.item_rarity.legendary, "Lengendary Torso", "Simple Torso"));
        AddItem(im.CreateObject<Shield>(Item.item_rarity.uncommon, "Uncommon Shield", "Simple Shield"));
        AddItem(im.CreateObject<Sword>(Item.item_rarity.epic, "Epic Sword", "Simple Sword"));
        AddItem(im.CreateObject<Axe>(Item.item_rarity.rare, "Rare Axe", "Simple Axe"));
        AddItem(im.CreateObject<Boots>(Item.item_rarity.common, "Common Boots", "Simple Boots"));
        AddItem(im.CreateObject<Torso>(Item.item_rarity.legendary, "Lengendary Torso", "Simple Torso"));
        AddItem(im.CreateObject<Shield>(Item.item_rarity.uncommon, "Uncommon Shield", "Simple Shield"));
        AddItem(im.CreateObject<Sword>(Item.item_rarity.epic, "Epic Sword", "Simple Sword"));
        AddItem(im.CreateObject<Axe>(Item.item_rarity.rare, "Rare Axe", "Simple Axe"));
        AddItem(im.CreateObject<Boots>(Item.item_rarity.common, "Common Boots", "Simple Boots"));
        AddItem(im.CreateObject<Torso>(Item.item_rarity.legendary, "Lengendary Torso", "Simple Torso"));
        AddItem(im.CreateObject<Shield>(Item.item_rarity.uncommon, "Uncommon Shield", "Simple Shield"));
        for (int i = 0; i < 10; i++)
            AddItem(im.CreateObject<Axe>());
        for (int i = 0; i < 10; i++)
            AddItem(im.CreateObject<Boots>());
        for (int i = 0; i < 10; i++)
            AddItem(im.CreateObject<Shield>());
        for (int i = 0; i < 10; i++)
            AddItem(im.CreateObject<Sword>());
        for (int i = 0; i < 10; i++)
            AddItem(im.CreateObject<Torso>());
        //DisplayInventory();
        //DisplayList(GetItemsByType<Armor>());
    }

}
