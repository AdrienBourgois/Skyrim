using UnityEngine;

public class EnemyController : ACharacterController
{
    #region Delegates and events
    public delegate void DelegateAction();
    public event DelegateAction OnLeftDown;
    public event DelegateAction OnLeftUp;
    public event DelegateAction OnRightDown;
    #endregion

    protected override void Start()
    {
        base.Start();
        characterWeapons.SetCharacter(character);

        ControllerDrawSheathSword();
    }

    protected override void Update()
    {
        ResetTriggers();

        if (paused)
        {
            ControllerMove(0f, 0f);
            return;
        }

        UpdateIa();
    }

    private void UpdateIa()
    {

        //ControllerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //if (Input.GetButtonDown("CastSpell"))
        //    ControllerCastSpell();

        #region Hands actions
            //ControllerRightHand();

            //ControllerLeftHand(true);
            //ControllerLeftHand(false);

            //ControllerDrawSheathSword();
        #endregion
    }

    #region Controller

    protected override void ControllerRightHand()
    {
        base.ControllerRightHand();

        if (OnRightDown != null)
            OnRightDown.Invoke();
    }

    protected override void ControllerLeftHand(bool _bIsPressed = true)
    {
        base.ControllerLeftHand(_bIsPressed);

        if (_bIsPressed)
        {
            if (OnLeftDown != null)
                OnLeftDown.Invoke();
        }
        else
        {
            if (OnLeftUp != null)
                OnLeftUp.Invoke();
        }
    }

    public override void ControllerUse()
    {
        RaycastHit hit;
        // TODO: global(?) variable for max distance
        const float useMaxDistance = 3f;
        if (Physics.Raycast(target.position, target.forward, out hit, useMaxDistance, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            IUsableObject usableCollider = hit.collider.GetComponent<IUsableObject>();

            if (usableCollider != null)
            {
                usableCollider.OnUse(character);
            }
        }
    }

    #endregion

}
