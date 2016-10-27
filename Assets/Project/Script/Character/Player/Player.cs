public class Player : ACharacter
{
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
