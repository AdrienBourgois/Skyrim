using UnityEngine;
using System.Collections.Generic;

public class Characteristics
{
    private Dictionary<string, float> characDict = new Dictionary<string, float>();

    #region Attack

    private float attack;
    public float Attack
    { get { return attack; } set { attack = value; } }

    #endregion

    #region Defense

    private float defense;
    public float Defense
    { get { return defense; } set { defense = value; } }

    #endregion

    #region Weight

    private float playerWeight;
    public float PlayerWeight
    { get { return playerWeight; } set { playerWeight = value; } }

    private float weight;
    public float Weight
    { get { return weight; } set { weight = value; } }

    #endregion

    #region Health

    private float maxHealth;
    public float MaxHealth
    { get { return maxHealth; } set { maxHealth = value; } }

    private float healthRegeneration;
    public float HealthRegeneration
    { get { return healthRegeneration; } set { healthRegeneration = value; } }

    private float health;
    public float Health
    { get { return health; } set { health = value; } }

    #endregion

    #region Mana

    private float maxMana;
    public float MaxMana
    { get { return maxMana; } set { maxMana = value; } }

    private float mana;
    public float Mana
    { get { return mana; } private set { mana = value; } }

    #endregion

    #region SpellPower

    private float spellPower;
    public float SpellPower
    { get { return spellPower; } set { spellPower = value; } }

    #endregion

    #region Precision

    private float precision;
    public float Precision
    { get { return precision; } set { precision = value; } }

    #endregion

    #region AttackSpeed

    private float attackSpeed;
    public float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value; } }

    #endregion

    public void UpdateCharacDict()
    {
        characDict["Attack"] = attack;
        characDict["Defense"] = defense;
        characDict["PlayerWeigth"] = playerWeight;
        characDict["Weight"] = weight;
        characDict["MaxHealth"] = maxHealth;
        characDict["Health"] = health;
        characDict["HealthRegeneration"] = healthRegeneration;
        characDict["MaxMana"] = maxMana;
        characDict["Mana"] = mana;
        characDict["SpellPower"] = spellPower;
        characDict["Precision"] = precision;
        characDict["AttackSpeed"] = attackSpeed;
    }

    public void RegenFullHealthAndMana()
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }

    public float GetCharacFromString(string characName)
    {
        if (!characDict.ContainsKey(characName))
        {
            Debug.Log("Characteristics.GetCharacFromString() try to call CharacDict with invalid key : " + characName);
            return -1;
        }

        return characDict[characName];
    }
}
