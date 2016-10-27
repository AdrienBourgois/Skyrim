using UnityEngine;

public class Player : ACharacter
{
    #region Exp
    private int xp = 0;
    public int Xp
    {
        get { return xpToLevelUp; }
        private set { xp = value; }
    }

    private int xpToLevelUp = 100;
    public int XpToLevelUp { get { return xpToLevelUp; } }
    #endregion

    private void Awake()
    {
    //    GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");

    //    if (playerArray.Length > 1)
    //        Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();

        UnitSpells.PlayerBasicSpellInit();
    }

    private void LevelUp()
    {
        if (Xp < xpToLevelUp)
            return;

        Xp -= xpToLevelUp;
        xpToLevelUp *= 2;
    }

    private int attributePointToAssign = 10;
    public int AttributePointToAssign
    {
        get { return attributePointToAssign; }
        set {   if (value >= 0)
                attributePointToAssign = value; }
    }
}
