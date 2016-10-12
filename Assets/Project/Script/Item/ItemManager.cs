using UnityEngine;
using System;

public class ItemManager : MonoBehaviour {
    
    public T CreateObject<T>(Item.item_rarity _rarity, string _name, string _description) where T : Item, IInstanciableItem, new()
    {
        T item = new T();
        item.NameObject = _name;
        item.Description = _description;
        item.Level = UnityEngine.Random.Range(1, 50);
        item.Rarity = _rarity;
        item.Instantiate();
        return item;
    }

    public T CreateObject<T>() where T : Item, IInstanciableItem, new()
    {
        T item = new T();
        item.Level = UnityEngine.Random.Range(1, 50);
        item.Rarity = GetRandRarity();
        item.SetRandName();
        item.Instantiate();
        return item;
    }

    Item.item_rarity GetRandRarity()
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
}
