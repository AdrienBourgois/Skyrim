using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Contained in the main camera, contains assets and weapon models. Need to be initialized.
/// </summary>
public class PlayerWeapons : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private GameObject leftHandObject = null;

    [SerializeField]
    private GameObject rightHandObject = null;
    #endregion

    private WeaponAnchor leftHand = null;
    private WeaponAnchor rightHand = null;

    // Use this for initialization
    void Start()
    {
        if (leftHandObject == null || rightHandObject == null)
            Debug.LogError("PlayerWeapons.Start() - leftHandObject and rightHandObject should be initialized.");

        leftHand = leftHandObject.GetComponent<WeaponAnchor>();
        if (leftHand == null)
            Debug.LogError("PlayerWeapons.Start() - couldn't get component of type WeaponAnchor in leftHandObject");

        rightHand = rightHandObject.GetComponent<WeaponAnchor>();
        if (rightHand == null)
            Debug.LogError("PlayerWeapons.Start() - couldn't get component of type WeaponAnchor in rightHandObject");

        leftHand.gameObject.SetActive(false);
        rightHand.gameObject.SetActive(false);
    }

    public void SetController(PlayerController playerController)
    {
        playerController.OnLeftDown += LeftDown;
        playerController.OnLeftUp += LeftUp;
        playerController.OnRightDown += RightDown;

        Animator playerAnimator = playerController.GetComponent<Animator>();
        if (playerAnimator == null)
            Debug.LogError("PlayerWeapons.SetController() - couldn't get component of type Animator in PlayerController");
        CharacterSwitchWeapon[] smb = playerAnimator.GetBehaviours<CharacterSwitchWeapon>();
        if (smb == null)
            Debug.LogError("PlayerWeapons.SetController() - couldn't get behaviours of type CharacterSwitchWeapon in playerAnimator");
        foreach (CharacterSwitchWeapon oneSmb in smb)
            oneSmb.OnSwitch += SwitchWeapon;
    }

    public void SetPlayer(Player player)
    {
        player.OnChangedWeapons += SetWeapons;
    }

    private void SetWeapons(Item leftWeapon, Item rightWeapon)
    {
        leftHand.SetWeapon(leftWeapon);
        rightHand.SetWeapon(rightWeapon);
    }

    private void LeftDown()
    {
        leftHand.Trigger();
    }

    private void LeftUp()
    {
        leftHand.TriggerBack();
    }

    private void RightDown()
    {
        rightHand.Trigger();
    }

    private void SwitchWeapon()
    {
        leftHand.Switch();
        rightHand.Switch();
    }
}
