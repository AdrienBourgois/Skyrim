using System;
using UnityEngine;

public class Shield : Armor, IEquipableItem, IInstanciableItem
{
    public void Equip()
    {
        Player player = LevelManager.Instance.Player;
        player.LeftHand = this;
        player.CharacterStats.SetCharacteristics(player);
        Equipped = player;
    }

    public void Instantiate()
    {
        Type = ItemType.Armor;
        float powerLvl = (float)Rarity;

        SetRandAttributes();

        float baseArmorValue = Mathf.Floor(Mathf.Exp(Level / (6 - powerLvl / 8)) * 4);
        float min = baseArmorValue * (1 - RangeOfGeneration / 100);
        float max = baseArmorValue * (1 + RangeOfGeneration / 100);
        Defense = (int)UnityEngine.Random.Range(min, max);

        Price = (int)(Mathf.Sqrt(Defense) * (10 + powerLvl * 6));

        Weight = 15 + (int)Rarity;

        PrefabPath = "Weapons/Shield";
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Shield");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
