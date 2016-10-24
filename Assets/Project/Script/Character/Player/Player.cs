using UnityEngine;
using System.Collections;

public class Player : ACharacter
{
    public delegate void DelegateWeapons(Item leftWeapon, Item rightWeapon);
    public event DelegateWeapons OnChangedWeapons;

    #region Equipement

    Weapon rightHand;
    public Weapon RightHand
    {
        get { return rightHand; }
        set { rightHand = value; }
    }

    Shield leftHand;
    public Shield LeftHand
    {
        get { return leftHand; }
        set { leftHand = value; }
    }

    Helmet helmet;
    public Helmet Helmet
    {
        get { return helmet; }
        set { helmet = value; }
    }

    Torso torso;
    public Torso Torso
    {
        get { return torso; }
        set { torso = value; }
    }

    Boots boots;
    public Boots Boots
    {
        get { return boots; }
        set { boots = value; }
    }

    #endregion

    #region Exp
    int xp = 0;
    public int Xp { get { return xp; } }


    int xpToLevelUp = 100;
    public int XpToLevelUp { get { return xpToLevelUp; } }
    #endregion


    protected override void Start()
    {
        base.Start();

        PlayerWeapons playerWeapons = transform.FindChild(GameManager.c_weaponChildName).GetComponent<PlayerWeapons>();
        if (playerWeapons == null)
            Debug.LogError("Player.Start() - could not find child of name \"" + GameManager.c_weaponChildName + "\" of type PlayerWeapons");
        playerWeapons.SetPlayer(this);

        UnitSpells.PlayerBasicSpellInit();
    }
   
    void LevelUp()
    {
        if (xp < xpToLevelUp)
            return;

        xp -= xpToLevelUp;
        xpToLevelUp *= 2;
    }

    int attributePointToAssign = 10;
    public int AttributePointToAssign
    {
        get { return attributePointToAssign; }
        set
        {
            if (value >= 0)
                attributePointToAssign = value;
        }
    }
}
