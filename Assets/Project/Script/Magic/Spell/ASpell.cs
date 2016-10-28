using UnityEngine;

public abstract class ASpell : MonoBehaviour {

    #region SerializeField
    [SerializeField]
    protected float lifeTime = 10f;
    #endregion

    protected SpellProperty spellProperty;
    protected ACharacterController selfController;

    public void SetController(ACharacterController _controller)
    {
        selfController = _controller;
    }

    public void SetProperty(SpellProperty _spellProp)
    {
        spellProperty = _spellProp;
    }

    public abstract void Activate();

}
