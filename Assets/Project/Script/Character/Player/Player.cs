using UnityEngine;
using System.Collections;

public class Player : ACharacter
{
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
