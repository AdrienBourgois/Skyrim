﻿using UnityEngine;
using System.Collections;

public class Characteristics : MonoBehaviour {

    #region SerializeField

    [SerializeField]
    private int attack;

    [SerializeField]
    private int defense;

    [SerializeField]
    private float weight;

    [SerializeField]
    private int health;

    [SerializeField]
    private int mana;

    [SerializeField]
    private int spellPower;

    [SerializeField]
    private float precision;

    [SerializeField]
    private float attackSpeed;

    #endregion

    #region Attack


    public int Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    #endregion

    #region Defense

    public int Defense
    {
        get
        {
            return defense;
        }

        set
        {
            defense = value;
        }
    }

    #endregion

    #region Weight

    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }

    #endregion

    #region Health
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }
    #endregion

    #region Mana
    public int Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
        }
    }
    #endregion

    #region SpellPower
    public int SpellPower
    {
        get
        {
            return spellPower;
        }

        set
        {
            spellPower = value;
        }
    }
    #endregion

    #region Precision
    public float Precision
    {
        get
        {
            return precision;
        }

        set
        {
            precision = value;
        }
    }
    #endregion

    #region AttackSpeed
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }

        set
        {
            attackSpeed = value;
        }
    }
    #endregion


    void Start () {
	
	}
	
	void Update () {
	
	}


}