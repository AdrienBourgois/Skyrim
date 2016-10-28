public class Player : ACharacter
{
    #region Exp
    public int Xp { get; private set; }

    private int xpToLevelUp = 100;
    public int XpToLevelUp { get { return xpToLevelUp; } }
    #endregion

    private int attributePointToAssign = 10;
    public int AttributePointToAssign
    {
        get { return attributePointToAssign; }
        set
        {
            if (value >= 0)
                attributePointToAssign = value;
        }
    }

    protected override void Start()
    {
        base.Start();

        UnitSpells.PlayerBasicSpellInit();
    }

    private void LevelUpCheck()
    {
        if (Xp < xpToLevelUp)
            return;

        Xp -= xpToLevelUp;
        xpToLevelUp *= 2;
    }

    public override void EarnXp(int _xpReward)
    {
        Xp += _xpReward;
        LevelUpCheck();
    }

    protected override void OnDeath()
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Death);
        Destroy(gameObject);
        Destroy(FindObjectOfType<Compass>());
    }
}
