public class Enemy : ACharacter
{
    protected override void Start()
    {
        base.Start();
        Debug.Log()
        UnitSpells.EnemyBasicSpellInit();
    }
}