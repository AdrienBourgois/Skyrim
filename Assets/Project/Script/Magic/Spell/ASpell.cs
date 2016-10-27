using UnityEngine;

public abstract class ASpell : MonoBehaviour {

    #region SerializeField
    [SerializeField]
    protected float lifeTime = 10f;

    [SerializeField]
    protected float projectileSpeed = 10f;
    #endregion

    protected SpellProperty spellProperty;
    protected ACharacterController selfController;

    public void SetController(ACharacterController controller)
    {
        selfController = controller;
    }

    public void SetProperty(SpellProperty spellProp)
    {
        spellProperty = spellProp;
    }

    public abstract void Activate();

}
