public class Player : ACharacter
{
    #region Equipement

    public Weapon RightHand { get; set; }

    public Shield LeftHand { get; set; }

    public Helmet Helmet { get; set; }

    public Torso Torso { get; set; }

    public Boots Boots { get; set; }

    #endregion

    #region Exp

    public int Xp { get; private set; }

    private int xpToLevelUp = 100;
    public int XpToLevelUp { get { return xpToLevelUp; } }
    #endregion

    protected override void Start()
    {
        base.Start();

        //PlayerWeapons playerWeapons = transform.FindChild(GameManager.c_weaponChildName).GetComponent<PlayerWeapons>();
        //if (playerWeapons == null)
        //    Debug.LogError("Player.Start() - could not find child of name \"" + GameManager.c_weaponChildName + "\" of type PlayerWeapons");
        //playerWeapons.SetPlayer(this);

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

    public Player()
    {
        Xp = 0;
    }

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
