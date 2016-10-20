using UnityEngine;
using System.Collections;
using System;

public class WeaponAnchor : MonoBehaviour
{
    private Item weapon = null;
    private bool bIsBlocking = false;
    public bool IsBlocking
    {
        get { return bIsBlocking; }
    }

    private delegate void DelegateItemType();
    private event DelegateItemType OnUpdate = () => { };
    private event DelegateItemType OnTrigger = () => { };
    private event DelegateItemType OnTriggerBack = () => { };

    void Start()
    {
        weapon = ItemManager.CreateObject<Sword>();
    }

    public void SetWeapon(Item _weapon)
    {        
        weapon = _weapon;

        if (weapon is Sword)
        {
            OnTrigger = TriggerSword;
            OnTriggerBack = TriggerBackSword;
        }
        else if (weapon is Axe)
        {
            OnTrigger = TriggerAxe;
            OnTriggerBack = TriggerBackAxe;
        }
        else if (weapon is Shield)
        {
            OnTrigger = TriggerShield;
            OnTriggerBack = TriggerBackShield;
        }
    }

    #region Trigger
    public void Trigger()
    {
        if (weapon == null || gameObject.activeSelf == false)
            return;
        OnTrigger.Invoke();
    }

    private void TriggerShield()
    {
        bIsBlocking = true;
        //TODO: set Shield block
    }

    private void TriggerSword()
    {
        //TODO: make Sword attack
    }

    private void TriggerAxe()
    {
        //TODO: make Axe attack
    }
    #endregion

    #region TriggerBack
    public void TriggerBack()
    {
        if (weapon == null || gameObject.activeSelf == false)
            return;
        OnTriggerBack.Invoke();
    }

    private void TriggerBackShield()
    {
        bIsBlocking = false;
        //TODO: set Shield back
    }

    private void TriggerBackSword()
    {
        // Do nothing
    }

    private void TriggerBackAxe()
    {
        // Do nothing
    }
    #endregion

    public void Switch()
    {
        if (weapon == null)
            return;

        if (gameObject.activeSelf == true)
        {
            // TODO: make animation
            gameObject.SetActive(false);
        }
        else
        {
            // TODO: make animation
            gameObject.SetActive(true);
        }
    }
}
