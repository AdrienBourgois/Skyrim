using UnityEngine;

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
    protected int cost;
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

        if (memberName == "Power")
            return power.ToString();

        if (memberName == "Cost")
            return cost.ToString();

        if (memberName == "Description")
            return description;

        return "none";
    }
}
