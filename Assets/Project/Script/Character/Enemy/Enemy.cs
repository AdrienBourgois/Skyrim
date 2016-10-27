using UnityEngine;

public class Enemy : ACharacter, IHitable
{
    #region Equipement

    public Weapon RightHand { get; set; }

    public Shield LeftHand { get; set; }

    public Helmet Helmet { get; set; }

    public Torso Torso { get; set; }

    public Boots Boots { get; set; }

    #endregion

    protected override void Start()
    {
        base.Start();

        UnitSpells.EnemyBasicSpellInit();
    }

    public void OnHit(ACharacter entity)
    {
        transform.position += Vector3.up;
    }
}
