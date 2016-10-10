using UnityEngine;
using System.Collections;
using System;

public class Armor : Item, IEquipableItem, IInstanciableItem
{
    protected int armor_value = 0;

    public void Equip()
    {
        throw new NotImplementedException();
    }

    public void Instantiate()
    {

    }
}
