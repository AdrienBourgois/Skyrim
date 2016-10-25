using UnityEngine;
using System;
using System.Collections.Generic;

public class MagicInventory
{
    private List<SpellProperty> magicList = new List<SpellProperty>();
    public List<SpellProperty> MagicList
    {
       get { return magicList; }
    }

    public void PlayerBasicSpellInit()
    {
        magicList.Add(MagicManager.Instance.CreateMagic<FireSpell>("FireBall", 
                                                                    50f,
                                                                    20,
                                                                    10f,
                                                                    "Cast a Powerfull Fireball which can ignite the opponent",
                                                                    MagicManager.MagicID.Fire,
                                                                    MagicManager.MagicType.Light));

        magicList.Add(MagicManager.Instance.CreateMagic<HealSpell>("Heal",
                                                                    30f,
                                                                    10,
                                                                    2,
                                                                    "Heal Yourself with a bright wave of light",
                                                                    MagicManager.MagicID.Heal,
                                                                    MagicManager.MagicType.Light));

        magicList.Add(MagicManager.Instance.CreateMagic<HealSpell>("Invisibility",
                                                                    0,
                                                                    50,
                                                                    30,
                                                                    "Cast the power of the dark night to become Invisible ... But your weapon are still visible ...",
                                                                    MagicManager.MagicID.Heal,
                                                                    MagicManager.MagicType.Medium));
    }


    public void EnemyBasicSpellInit()
    {
        magicList.Add(MagicManager.Instance.CreateMagic<FireSpell>("FireBall",
                                                                     30f,
                                                                     20,
                                                                     10f,
                                                                     "Cast a Powerfull Fireball which can ignite the opponent",
                                                                     MagicManager.MagicID.Fire,
                                                                     MagicManager.MagicType.Light));
    }

}
