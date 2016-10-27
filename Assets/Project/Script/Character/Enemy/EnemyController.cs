using UnityEngine;
using System.Collections;

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
        target = FindObjectOfType<Cam>().transform;

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

        UpdateIA();
    }

    private void UpdateIA()
    {

        ControllerMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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

    protected override void ControllerLeftHand(bool bIsPressed = true)
    {
        base.ControllerLeftHand(bIsPressed);

        if (bIsPressed)
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
        float useMaxDistance = 3f;
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
