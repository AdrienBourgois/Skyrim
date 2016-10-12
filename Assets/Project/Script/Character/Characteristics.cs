using UnityEngine;
using System.Collections;

public class Characteristics
{

    #region SerializeField

    [SerializeField]
    private float attack;

    [SerializeField]
    private float defense;

    [SerializeField]
    private float weight;

    [SerializeField]
    private float health;

    [SerializeField]
    private float mana;

    [SerializeField]
    private float spellPower;

    [SerializeField]
    private float precision;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private float healthRegeneration;

    [SerializeField]
    private float playerWeight;

   
    #endregion

    #region Attack

    public float Attack
    { get { return attack; } set { attack = value; } }

    #endregion

    #region Defense

    public float Defense
    { get { return defense; } set { defense = value; } }

    #endregion

    #region Weight

    public float PlayerWeight
    { get { return playerWeight; } set { playerWeight = value; } }

    public float Weight
    { get { return weight; } set { weight = value; } }

    #endregion

    #region Health

    private float maxHealth;

    public float MaxHealth
    { get { return maxHealth; } set { maxHealth = value; } }

    public float HealthRegeneration
    { get { return healthRegeneration; } set { healthRegeneration = value; } }

    public float Health
    { get { return health; } set { health = value; } }

    #endregion

    #region Mana

    private float maxMana;
    public float MaxMana
    { get { return maxMana; } set { maxMana = value; } }

    public float Mana
    { get { return mana; } set { mana = value; } }

    #endregion

    #region SpellPower

    public float SpellPower
    { get { return spellPower; } set { spellPower = value; } }

    #endregion

    #region Precision

    public float Precision
    { get { return precision; } set { precision = value; } }

    #endregion

    #region AttackSpeed

    public float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value; } }

   









    #endregion



    public void Init(float attack, float defense, float weight, float health, float mana, float spellPower, float precision, float attackSpeed)
    {
        Attack = attack;
        Defense = defense;
        Weight = weight;
        MaxHealth = health;
        Health = health;
        MaxMana = mana;
        Mana = maxMana;
        SpellPower = spellPower;
        Precision = precision;
     
    }



}
