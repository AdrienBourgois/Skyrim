using UnityEngine;
using System.Collections;

public class Attributes
{
      
    private int maxAttributePoints = 110;
    public int MaxAttributePoints
    { get { return maxAttributePoints; } set { maxAttributePoints = value; } }

    #region Strength

    private int strength = 10;
    public int Strength
    { get { return strength; } set { strength = value; } }

    #endregion

    #region Constitution

    private int constitution = 10;
    public int Constitution
    {
        get { return constitution; } set { constitution = value; } }

    #endregion

    #region Intelligence

    private int intelligence = 10;
    public int Intelligence
    {
        get { return intelligence; } set { intelligence = value; } }

    #endregion

    #region Dexterity

    private int dexterity = 10;
    public int Dexterity
    {
        get { return dexterity; } set { dexterity = value; } }

    #endregion


    public int GetAttribFromString(string attribName)
    {
        switch (attribName)
        {
            case "Strength":
                return Strength;

            case "Constitution":
                return Constitution;

            case "Intelligence":
                return Intelligence;

            case "Dexterity":
                return Dexterity;

            default:
                Debug.Log("Attributes.GetAttribFromString() -> Can't find attribute named : " + attribName);
                return -1;
        }
    }
    public void SetAttribFromString(string attribName, int value)
    {
        switch (attribName)
        {
            case "Strength":
                strength = value;
                break;

            case "Constitution":
                constitution = value;
                break;

            case "Intelligence":
                intelligence = value;
                break;

            case "Dexterity":
                dexterity = value;
                break;

            default:
                Debug.Log("Attributes.GetAttribFromString() -> Can't find attribute named : " + attribName);
                break;
        }
    }
}
