using UnityEngine;
using System.Collections;

public class Player : ACharacter
{
    public delegate void DelegateWeapons(Item leftWeapon, Item rightWeapon);
    public event DelegateWeapons OnChangedWeapons;
}
