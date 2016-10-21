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
    private GameObject leftHandAnchor = null;

    [SerializeField]
    private GameObject rightHandAnchor = null;
    #endregion

    private WeaponAnchor leftHand = null;
    private WeaponAnchor rightHand = null;

    // Use this for initialization
    void Start()
    {
        if (leftHandAnchor == null || rightHandAnchor == null)
            Debug.LogError("PlayerWeapons.Start() - leftHandAnchor and rightHandAnchor should be initialized.");

        leftHand = leftHandAnchor.AddComponent<WeaponAnchor>();
        if (leftHand == null)
            Debug.LogError("PlayerWeapons.Start() - couldn't get component of type WeaponAnchor in leftHandAnchor");

        rightHand = rightHandAnchor.AddComponent<WeaponAnchor>();
        if (rightHand == null)
            Debug.LogError("PlayerWeapons.Start() - couldn't get component of type WeaponAnchor in rightHandAnchor");

        leftHand.SetWeapon(ItemManager.Instance.CreateObject<Shield>());
        rightHand.SetWeapon(ItemManager.Instance.CreateObject<Sword>());
    }

    public void SetController(PlayerController playerController)
    {
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

    private void SwitchWeapon()
    {
        leftHand.Switch();
        rightHand.Switch();
    }
}
