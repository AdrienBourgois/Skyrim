using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

    private int strength;
    private int constitution;
    private int intelligence;
    private int dexterity;

    

    void Start () {
	
	}
	
	void Update () {
	
	}


    #region Strength

    public int Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }




    #endregion

    #region Constitution

    public int Constitution
    {
        get
        {
            return constitution;
        }

        set
        {
            constitution = value;
        }
    }



    #endregion

    #region Intelligence

    public int Intelligence
    {
        get
        {
            return intelligence;
        }

        set
        {
            intelligence = value;
        }
    }



    #endregion

    #region Dexterity

    public int Dexterity
    {
        get
        {
            return dexterity;
        }

        set
        {
            dexterity = value;
        }
    }

    #endregion
}
