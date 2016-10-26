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
        magicList.Add(MagicManager.Instance.CreateSpellProperties(MagicManager.MagicID.Fireball,
                                                        MagicManager.MagicType.Medium,
                                                        50f,
                                                        20,
                                                        "Cast a Powerfull Fireball which can ignite the opponent"));

        magicList.Add(MagicManager.Instance.CreateSpellProperties(MagicManager.MagicID.Heal,
                                                        MagicManager.MagicType.Self,
                                                        30f,
                                                        10,
                                                        "Heal Yourself with a bright wave of light"));

        magicList.Add(MagicManager.Instance.CreateSpellProperties(MagicManager.MagicID.Invisibility,
                                                        MagicManager.MagicType.Self,
                                                        0,
                                                        50,
                                                        "Cast the power of the dark night to become Invisible ... But your weapon are still visible ..."));
    }


    public void EnemyBasicSpellInit()
    {
        magicList.Add(MagicManager.Instance.CreateSpellProperties(MagicManager.MagicID.Fireball,
                                                        MagicManager.MagicType.Light,
                                                        30f,
                                                        20,
                                                        "Cast a Powerfull Fireball which can ignite the opponent"));
    }

}
