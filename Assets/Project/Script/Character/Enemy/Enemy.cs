using UnityEngine;

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

    public override void OnHit(ACharacter _character, float _spellDamages)
    {
        lastCollidingChar = _character;
        base.OnHit(_character, _spellDamages);
    }

    protected override void OnDeath()
    {
        if (lastCollidingChar != null)
            lastCollidingChar.EarnXp(XpReward);
        
        int rnd = Random.Range(0, 3);
        if (rnd == 0)
        {
            TreasureChest drop = Instantiate(ResourceManager.Instance.Load<TreasureChest>("Dungeon/ChestDrop"));
            drop.transform.position = transform.position;
        }

        AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Calm);

        Destroy(gameObject);
    }
}