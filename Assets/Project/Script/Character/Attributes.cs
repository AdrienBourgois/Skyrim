using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour {

    private int strength = 10;
    private int constitution = 10;
    private int intelligence = 10;
    private int dexterity = 10;
    private int maxAttributePoints = 100;
    

    void Start () {
	
	}
	
	void Update () {
	
	}


    #region Strength

    public int Strength
    { get { return strength; } set { strength = value; } }




    #endregion

    #region Constitution

    public int Constitution
    {
        get { return constitution; } set { constitution = value; } }



    #endregion

    #region Intelligence

    public int Intelligence
    {
        get { return intelligence; } set { intelligence = value; } }



    #endregion

    #region Dexterity

    public int Dexterity
    {
        get { return dexterity; } set { dexterity = value; } }



    #endregion

    public int MaxAttributePoints
    { get { return maxAttributePoints; } set { maxAttributePoints = value; } }
}
