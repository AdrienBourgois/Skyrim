using UnityEngine;
using System.Collections;

public abstract class AMagic : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    float power = 10f;

    [SerializeField]
    float lifeTime = 10f;

    [SerializeField]
    MagicManager.MagicID id = MagicManager.MagicID.NONE;

    [SerializeField]
    MagicManager.MagicType type = MagicManager.MagicType.None;
    #endregion

    protected ACharacter selfCharacter = null;

    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public virtual void Activate()
    {

    }

    protected virtual void OnDestroy()
    {

    }
}
