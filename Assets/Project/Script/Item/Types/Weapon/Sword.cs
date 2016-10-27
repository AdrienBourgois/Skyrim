using UnityEngine;
using System;

public class Sword : Weapon, IEquipableItem, IInstanciableItem
{
    public void Equip()
    {
        Player player = LevelManager.Instance.Player;
        player.RightHand = this;
        player.CharacterStats.SetCharacteristics(player);
        Equipped = player;
    }

    public void Instantiate()
    {
        Type = ItemType.Weapon;
        weaponType = weapon_type.Sword;
        float powerLvl = (float)Rarity;

        SetRandAttributes();

        float baseDamageValue = Mathf.Floor(Mathf.Exp(Level / (6 - powerLvl / 8)) * 10);
        float min = baseDamageValue * (1 - (rangeOfGeneration / 100));
        float max = baseDamageValue * (1 + (rangeOfGeneration / 100));
        Damage = (int)UnityEngine.Random.Range(min, max);

        Price = (int)(Mathf.Sqrt(Damage) * (10 + (powerLvl * 6)));
        Weight = 15 + (int)Rarity;

        PrefabPath = "Weapons/Sword";
    }

    public void SetRandName()
    {
        NameObject = NameGenerator.GenerateNewName((int)Rarity, "Sword");
    }

    public void SetRandDescription()
    {
        throw new NotImplementedException();
    }
}
