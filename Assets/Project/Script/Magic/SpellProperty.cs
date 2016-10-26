using UnityEngine;
using System.Collections;

public class SpellProperty
{
    #region Serialized Fields
    [SerializeField]
    protected string magicName;
    public string MagicName
    {
        get { return magicName; }
    }

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
    protected float lifeTime = 10f;

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

    //protected virtual void Start()
    //{
    //    Destroy(gameObject, lifeTime);
    //}


    
    //protected virtual void OnDestroy()
    //{

    //}

    public void Init(string name, float power, int cost, float lifeTime, string description, MagicManager.MagicID id, MagicManager.MagicType type)
    {
        this.magicName = name;
        this.power = power;
        this.cost = cost;
        this.lifeTime = lifeTime;
        this.description = description;
        this.id = id;
        this.type = type;
    }

    public string GetMemberStringFromString(string memberName)
    {
        if (memberName == "Name")
            return magicName;

        else if (memberName == "Power")
            return power.ToString();

        else if (memberName == "Cost")
            return cost.ToString();

        else if (memberName == "Description")
            return description.ToString();

        return "none";
    }
}
