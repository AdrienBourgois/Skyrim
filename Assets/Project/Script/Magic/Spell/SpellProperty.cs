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
    public MagicManager.MagicID Id
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

    public void Init(MagicManager.MagicID _id, MagicManager.MagicType _type , float _power, int _cost, string _description)
    {
        id = _id;
        type = _type;
        power = _power;
        cost = _cost;
        description = _description;
    }

    public string GetMemberStringFromString(string _memberName)
    {
        if (_memberName == "Name")
            return id.ToString();

        if (_memberName == "Power")
            return power.ToString();

        if (_memberName == "Cost")
            return cost.ToString();

        if (_memberName == "Description")
            return description;

        return "none";
    }
}
