using System;
using UnityEngine;

public class Enemy : ACharacter
{
    [SerializeField]
    private int xpReward = 10;
    public int XPReward { get { return xpReward; } }

    private ACharacter lastCollidingChar;

    protected override void Start()
    {
        base.Start();
        UnitSpells.EnemyBasicSpellInit();
    }

    public override void OnHit(ACharacter _character)
    {
        lastCollidingChar = _character;
        base.OnHit(_character);
    }

    protected override void OnDeath()
    {
        if (lastCollidingChar != null)
            lastCollidingChar.EarnXp(XPReward);
        // TODO: Instantiate(TREASURE);


        Destroy(gameObject);
    }
}