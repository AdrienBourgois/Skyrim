using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
    
    public T CreateObject<T>(Item.item_type _type, Item.item_rarity _rarity, string _name, string _description) where T : Item, IInstanciableItem, new()
    {
        T item = new T();
        item.Type = _type;
        item.NameObject = _name;
        item.Description = _description;
        item.Rarity = _rarity;
        item.Instantiate();
        return item;
    }
}
