﻿using UnityEngine;

public class Enemy : ACharacter
{
    [SerializeField]
    private int xpReward = 10;
    private int XpReward { get { return xpReward; } }

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
        AudioManager.Instance.PlaySound(AudioManager.ESoundType.Sword, transform.position);
    }

    protected override void OnDeath()
    {
        if (lastCollidingChar != null)
            lastCollidingChar.EarnXp(XpReward);
        // TODO: Instantiate(TREASURE);

        AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Game);

        Destroy(gameObject);
    }
}