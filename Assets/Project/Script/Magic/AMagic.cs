using UnityEngine;
using System.Collections;

public abstract class AMagic : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private float power = 10f;
    public float Power
    {
        get { return power; }
        protected set { power = value; }
    }

    [SerializeField]
    private float lifeTime = 10f;

    [SerializeField]
    private MagicManager.MagicID id = MagicManager.MagicID.NONE;
    public MagicManager.MagicID ID
    {
        get { return id; }
    }

    [SerializeField]
    private MagicManager.MagicType type = MagicManager.MagicType.None;
    public MagicManager.MagicType Type
    {
        get { return type; }
    }
    #endregion

    protected ACharacterController selfController = null;

    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetController(ACharacterController controller)
    {
        selfController = controller;
    }
    
    protected virtual void OnDestroy()
    {

    }

    public abstract void Activate();
}
