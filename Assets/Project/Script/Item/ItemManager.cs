﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemManager
{
    [Flags] public enum flags_generation
    {
        None = 0,

        Helmet = 1,
        Torso = 2,
        Shield = 4,
        Boots = 8,
        Armor = Helmet|Torso|Shield|Boots,

        Sword = 16,
        Axe = 32,
        Weapon = Sword|Axe,

        All_Type = Armor|Weapon,
    }

    static public List<Item> GenerateInventory(flags_generation flags = flags_generation.All_Type, int size = 60)
    {
        List<Item> inventory = new List<Item>();
        int flags_count = GetFlagsCount(flags);
        int objects_by_type = (int)Math.Ceiling((float)(size / flags_count));

        Debug.Log("Generate " + size + " objects of type(s) : " + flags.ToString() + " (" + objects_by_type + " per type)");

        System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

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

        watch.Stop();
        Debug.Log(inventory.Count + " objects generated in " + watch.ElapsedMilliseconds + " ms");

        return inventory;
    }

    static public T CreateObject<T>(Item.item_rarity _rarity, string _name, string _description) where T : Item, IInstanciableItem, new()
    {
        T item = new T();
        item.NameObject = _name;
        item.Description = _description;
        item.Level = UnityEngine.Random.Range(1, 51);
        item.Rarity = _rarity;
        item.Instantiate();
        return item;
    }

    static public T CreateObject<T>() where T : Item, IInstanciableItem, new()
    {
        T item = new T();
        item.Level = UnityEngine.Random.Range(1, 51);
        item.Rarity = GetRandRarity();
        item.SetRandName();
        item.Instantiate();
        return item;
    }

    static Item.item_rarity GetRandRarity()
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

    static int GetFlagsCount(flags_generation flags)
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
