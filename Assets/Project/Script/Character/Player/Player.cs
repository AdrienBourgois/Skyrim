using UnityEngine;
using System.Collections;

public class Player : ACharacter
{
    public delegate void DelegateWeapons(Item leftWeapon, Item rightWeapon);
    public event DelegateWeapons OnChangedWeapons;

    protected override void Start()
    {
        base.Start();

        PlayerWeapons playerWeapons = transform.FindChild(GameManager.c_weaponChildName).GetComponent<PlayerWeapons>();
        if (playerWeapons == null)
            Debug.LogError("Player.Start() - could not find child of name \"" + GameManager.c_weaponChildName + "\" of type PlayerWeapons");
        playerWeapons.SetPlayer(this);
    }
}
