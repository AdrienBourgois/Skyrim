using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    static private ItemManager instance;
    static public ItemManager Instance
    {
        get { return instance ?? (instance = FindObjectOfType<ItemManager>()); }
    }

    //TODO: to changed with IDs ?
    //private Dictionary<string, WeaponInstance> mapCachePrefab = new Dictionary<string, WeaponInstance>();

    [Flags] public enum FlagsGeneration
    {
        [Useless]
        None = 0,

        Helmet = 1,
        Torso = 2,
        Shield = 4,
        Boots = 8,
        Armor = Helmet | Torso | Shield | Boots,

        Sword = 16,
        Axe = 32,
        Weapon = Sword | Axe,

        AllType = Armor | Weapon
    }

    public List<Item> GenerateInventory(FlagsGeneration _flags = FlagsGeneration.AllType, int _size = 60)
    {
        List<Item> inventory = new List<Item>();
        int flagsCount = GetFlagsCount(_flags);
        int objectsByType = (int)Math.Ceiling((float)(_size / flagsCount));

        if ((_flags & FlagsGeneration.Helmet) != 0)
            for (int i = 0; i < objectsByType; i++)
                inventory.Add(CreateObject<Helmet>());
        if ((_flags & FlagsGeneration.Torso) != 0)
            for (int i = 0; i < objectsByType; i++)
                inventory.Add(CreateObject<Torso>());
        if ((_flags & FlagsGeneration.Shield) != 0)
            for (int i = 0; i < objectsByType; i++)
                inventory.Add(CreateObject<Shield>());
        if ((_flags & FlagsGeneration.Boots) != 0)
            for (int i = 0; i < objectsByType; i++)
                inventory.Add(CreateObject<Boots>());
        if ((_flags & FlagsGeneration.Sword) != 0)
            for (int i = 0; i < objectsByType; i++)
                inventory.Add(CreateObject<Sword>());
        if ((_flags & FlagsGeneration.Axe) != 0)
            for (int i = 0; i < objectsByType; i++)
                inventory.Add(CreateObject<Axe>());

        inventory.Sort();

        return inventory;
    }

    public WeaponInstance InstantiateItem(Item _item)
    {
        WeaponInstance itemPrefab = ResourceManager.Instance.Load<WeaponInstance>(_item.PrefabPath);

        return Instantiate(itemPrefab);
    }

    [Useless]
    public T CreateObject<T>(Item.ItemRarity _rarity, string _name, string _description) where T : Item, IInstanciableItem, new()
    {
        T item = new T
        {
            NameObject = _name,
            Description = _description,
            Level = UnityEngine.Random.Range(1, 51),
            Rarity = _rarity
        };
        item.Instantiate();
        return item;
    }

    public T CreateObject<T>() where T : Item, IInstanciableItem, new()
    {
        T item = new T
        {
            Level = UnityEngine.Random.Range(1, 51),
            Rarity = GetRandRarity()
        };
        item.SetRandName();
        item.Instantiate();
        return item;
    }

    private Item.ItemRarity GetRandRarity()
    {
        int score = UnityEngine.Random.Range(0, 100);
        if (score >= 98)
            return Item.ItemRarity.Legendary;
        if (score >= 90)
            return Item.ItemRarity.Epic;
        if (score >= 85)
            return Item.ItemRarity.Rare;
        if (score >= 50)
            return Item.ItemRarity.Uncommon;
        return Item.ItemRarity.Common;
    }

    private int GetFlagsCount(FlagsGeneration _flags)
    {
        int flagsCount = 0;

        while (_flags != 0)
        {
            _flags = _flags & (_flags - 1);
            flagsCount++;
        }

        return flagsCount;
    }
}
