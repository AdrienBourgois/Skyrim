using UnityEngine;
using System.Collections;

public class SpellProperty
{
    #region Serialized Fields
    [SerializeField]
    protected float power = 10f;
    public float Power
    {
        get { return power; }
    }

    [SerializeField]
    protected int cost = 0;
    public int Cost
    {
        get { return cost; }
    }

    [SerializeField]
    protected string description = "";
    public string Description
    {
        get { return description; }
    }

    [SerializeField]
    protected MagicManager.MagicID id = MagicManager.MagicID.NONE;
    public MagicManager.MagicID ID
    {
        get { return id; }
    }

    [SerializeField]
    protected MagicManager.MagicType type = MagicManager.MagicType.None;
    public MagicManager.MagicType Type
    {
        get { return type; }
    }
    #endregion

    public void Init(MagicManager.MagicID id, MagicManager.MagicType type , float power, int cost, string description)
    {
        this.id = id;
        this.type = type;
        this.power = power;
        this.cost = cost;
        this.description = description;
        
    }

    public string GetMemberStringFromString(string memberName)
    {
        if (memberName == "Name")
            return id.ToString();

        else if (memberName == "Power")
            return power.ToString();

        else if (memberName == "Cost")
            return cost.ToString();

        else if (memberName == "Description")
            return description.ToString();

        return "none";
    }
}
