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

        

        UnitSpells.PlayerBasicSpellInit();
    }

    void Update()
    {
        CharacterStats.BaseCharacteristics.Health -= 1;
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
