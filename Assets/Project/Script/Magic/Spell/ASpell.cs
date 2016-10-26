using UnityEngine;
using System.Collections;

public abstract class ASpell : MonoBehaviour {

    #region SerializeField
    [SerializeField]
    protected float lifeTime = 10f;

    [SerializeField]
    protected float projectileSpeed = 10f;

    [SerializeField ]
    protected SpellProperty spellProperty;
    #endregion


    protected ACharacterController selfController;

    public void SetController(ACharacterController controller)
    {
        selfController = controller;
    }

    public abstract void Activate();

}
