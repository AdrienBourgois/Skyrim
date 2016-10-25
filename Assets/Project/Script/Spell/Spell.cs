﻿using UnityEngine;

public class Spell
{
    private string name;
    private float power;
    private int cost;

    private string description;

    #region GetterProperties

    public string Name
    { get { return name; } }

    public float Power
    { get { return power; } }

    public int Cost
    { get { return cost; } }

    public string Description
    { get { return description; } }

    #endregion  
	
    public void Init(string name, float value, int cost, string description)
    {
        this.name = name;
        power = value;
        this.cost = cost;
        this.description = description;
    }

    public string GetMemberStringFromString(string memberName, bool useToDisplay)
    {
        if (memberName == "Name")
            return name;

        if (memberName == "Power")
        {
            if (useToDisplay)
                return Mathf.Abs(power).ToString();
            else
                return power.ToString();
        }

        else if (memberName == "Cost")
            return cost.ToString();

        else if (memberName == "Description")
            return description;

        return "none";
    }
}
