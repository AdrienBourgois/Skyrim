public class Enemy : ACharacter
{
    protected override void Start()
    {
        base.Start();
        UnitSpells.EnemyBasicSpellInit();
    }
}