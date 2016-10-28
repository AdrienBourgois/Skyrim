using System.Collections.Generic;

public class Characteristics
{
    public delegate void DelegateCharac();
    public event DelegateCharac OnDeath;

    private Dictionary<string, float> characDict = new Dictionary<string, float>();

    #region Attack

    public float Attack { get; set; }

    #endregion

    #region Defense

    public float Defense { get; set; }

    #endregion

    #region Weight

    public float PlayerWeight { get; set; }

    public float Weight { get; set; }

    #endregion

    #region Health

    public float MaxHealth { get; set; }

    public float HealthRegeneration { get; set; }

    private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health > MaxHealth)
                health = MaxHealth;
            if (health < 0)
            {
                health = 0;
                if (OnDeath != null)
                    OnDeath.Invoke();
            }
        }
    }

    #endregion

    #region Mana

    public float MaxMana { get; set; }

    private float mana;
    public float Mana
    {
        get { return mana; }
        set
        { mana = value;
            if (mana > MaxMana)
                mana = MaxMana;
            if (mana < 0)
                mana = 0;
        }
    }

    #endregion

    #region SpellPower

    public float SpellPower { get; set; }

    #endregion

    #region Precision

    public float Precision { get; set; }

    #endregion

    #region AttackSpeed

    public float AttackSpeed { get; set; }

    #endregion

    public Characteristics(int _value)
    {
        Attack = _value;
        Defense = _value;
        Weight = _value;
        MaxHealth = _value;
        Health = _value;
        HealthRegeneration = _value;
        MaxMana = _value;
        Mana = _value;
        SpellPower = _value;
        Precision = _value;
        AttackSpeed = _value;
    }

    public void UpdateCharacDict()
    {
        characDict["Attack"] = Attack;
        characDict["Defense"] = Defense;
        characDict["PlayerWeigth"] = PlayerWeight;
        characDict["Weight"] = Weight;
        characDict["MaxHealth"] = MaxHealth;
        characDict["Health"] = health;
        characDict["HealthRegeneration"] = HealthRegeneration;
        characDict["MaxMana"] = MaxMana;
        characDict["Mana"] = mana;
        characDict["SpellPower"] = SpellPower;
        characDict["Precision"] = Precision;
        characDict["AttackSpeed"] = AttackSpeed;
    }

    public void RegenFullHealthAndMana()
    {
        Health = MaxHealth;
        Mana = MaxMana;
    }

    public float GetCharacFromString(string _characName)
    {
        if (!characDict.ContainsKey(_characName))
            return -1;

        return characDict[_characName];
    }
}
