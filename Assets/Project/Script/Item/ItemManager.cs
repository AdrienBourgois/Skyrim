using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    static private ItemManager instance;
    static public ItemManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ItemManager>();
            return instance;
        }
    }

    private Dictionary<string, WeaponInstance> mapCachePrefab = new Dictionary<string, WeaponInstance>();

    [Flags] public enum flags_generation
    {
        None = 0,

        Helmet = 1,
        Torso = 2,
        Shield = 4,
        Boots = 8,
        Armor = Helmet | Torso | Shield | Boots,

        Sword = 16,
        Axe = 32,
        Weapon = Sword | Axe,

        All_Type = Armor | Weapon,
    }

    public List<Item> GenerateInventory(flags_generation flags = flags_generation.All_Type, int size = 60)
    {
        List<Item> inventory = new List<Item>();
        int flags_count = GetFlagsCount(flags);
        int objects_by_type = (int)Math.Ceiling((float)(size / flags_count));

        if ((flags & flags_generation.Helmet) != 0)
            for (int i = 0; i < objects_by_type; i++)
                inventory.Add(CreateObject<Helmet>());
        if ((flags & flags_generation.Torso) != 0)
            for (int i = 0; i < objects_by_type; i++)
                inventory.Add(CreateObject<Torso>());
        if ((flags & flags_generation.Shield) != 0)
            for (int i = 0; i < objects_by_type; i++)
                inventory.Add(CreateObject<Shield>());
        if ((flags & flags_generation.Boots) != 0)
            for (int i = 0; i < objects_by_type; i++)
                inventory.Add(CreateObject<Boots>());
        if ((flags & flags_generation.Sword) != 0)
            for (int i = 0; i < objects_by_type; i++)
                inventory.Add(CreateObject<Sword>());
        if ((flags & flags_generation.Axe) != 0)
            for (int i = 0; i < objects_by_type; i++)
                inventory.Add(CreateObject<Axe>());

        inventory.Sort();

        return inventory;
    }

    public WeaponInstance InstantiateItem(Item item)
    {
        WeaponInstance itemPrefab;

        if (!mapCachePrefab.TryGetValue(item.PrefabPath, out itemPrefab))
        {
            itemPrefab = Resources.Load<WeaponInstance>(item.PrefabPath);
            if (itemPrefab == null)
            {
                Debug.LogError("ItemManager.InstantiateItem() couldn't load prefab with path \"" + item.PrefabPath + "\"");
                return null;
            }
            mapCachePrefab.Add(item.PrefabPath, itemPrefab);
        }

        return Instantiate(itemPrefab);
    }
       
    public T CreateObject<T>(Item.item_rarity _rarity, string _name, string _description) where T : Item, IInstanciableItem, new()
    {
        T item = new T();
        item.NameObject = _name;
        item.Description = _description;
        item.Level = UnityEngine.Random.Range(1, 51);
        item.Rarity = _rarity;
        item.Instantiate();
        return item;
    }

    public T CreateObject<T>() where T : Item, IInstanciableItem, new()
    {
        T item = new T();
        item.Level = UnityEngine.Random.Range(1, 51);
        item.Rarity = GetRandRarity();
        item.SetRandName();
        item.Instantiate();
        return item;
    }

    private Item.item_rarity GetRandRarity()
    {
        int score = UnityEngine.Random.Range(0, 100);
        if (score >= 98)
            return Item.item_rarity.legendary;
        if (score >= 90)
            return Item.item_rarity.epic;
        if (score >= 85)
            return Item.item_rarity.rare;
        if (score >= 50)
            return Item.item_rarity.uncommon;
        return Item.item_rarity.common;
    }

    private int GetFlagsCount(flags_generation flags)
    {
        int flags_count = 0;

        while (flags != 0)
        {
            flags = flags & (flags - 1);
            flags_count++;
        }

        return flags_count;
    }
}
