using System;
using UnityEngine;

public class Helmet : Armor, IEquipableItem, IInstanciableItem
{
    public void Equip()
    {
        Player player = LevelManager.Instance.Player;
        player.Helmet = this;
        player.CharacterStats.SetCharacteristics(player);
        Equipped = player;
    }

    public void Instantiate()
    {
        Type = ItemType.Armor;
        ArmorType = armor_type.Helmet;
        float powerLvl = (float)Rarity;

        SetRandAttributes();

        float baseArmorValue = Mathf.Floor(Mathf.Exp(Level / (6 - powerLvl / 8)) * 3);
        float min = baseArmorValue * (1 - (rangeOfGeneration / 100));
        float max = baseArmorValue * (1 + (rangeOfGeneration / 100));
        Defense = (int)UnityEngine.Random.Range(min, max);

        Price = (int)(Mathf.Sqrt(Defense) * (10 + (powerLvl * 6)));

        Weight = 5 + (int)Rarity;
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Helmet");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
