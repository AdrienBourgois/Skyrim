using System.Collections.Generic;

public class Attributes
{
    private Dictionary<string, int> attribDict = new Dictionary<string, int>();

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

    public void UpdateAttribDict()
    {
        attribDict["Strength"] = strength;
        attribDict["Constitution"] = constitution;
        attribDict["Intelligence"] = intelligence;
        attribDict["Dexterity"] = dexterity;
    }

    public int GetAttribFromString(string _attribName)
    {
        if (!attribDict.ContainsKey(_attribName))
            return -1;

        return attribDict[_attribName];
    }
    public void SetAttribFromString(string _attribName, int _value)
    {
        switch (_attribName)
        {
            case "Strength":
                strength = _value;
                break;

            case "Constitution":
                constitution = _value;
                break;

            case "Intelligence":
                intelligence = _value;
                break;

            case "Dexterity":
                dexterity = _value;
                break;
        }
        UpdateAttribDict();
    }
}
