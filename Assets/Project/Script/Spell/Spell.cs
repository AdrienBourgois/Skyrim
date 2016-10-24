using UnityEngine;
using System.Collections;

public class Spell
{

    string name;
    float value;
    int cost;

    string description;

    #region GetterProperties

    public string Name
    { get { return name; } }

    public float Value
    { get { return value; } }

    public int Cost
    { get { return cost; } }

    public string Description
    { get { return description; } }

    #endregion  
	
    public void Init(string name, float value, int cost, string description)
    {
        this.name = name;
        this.value = value;
        this.cost = cost;
        this.description = description;
    }
}
