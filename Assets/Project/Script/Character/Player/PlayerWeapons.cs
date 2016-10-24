using UnityEngine;
using System.Collections;
using System;

public class PlayerWeapons : ACharacterWeapons
{
    public void SetPlayer(Player player)
    {
        player.OnChangedWeapons += SetWeapons;
    }
}
