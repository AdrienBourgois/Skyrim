using UnityEngine;
using System.Collections;

public class ACharacterWeapons : MonoBehaviour
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
            Debug.LogError("ACharacterWeapons.Start() - leftHandAnchor and rightHandAnchor should be initialized.");

        leftHand = leftHandAnchor.AddComponent<WeaponAnchor>();
        if (leftHand == null)
            Debug.LogError("ACharacterWeapons.Start() - couldn't get component of type WeaponAnchor in leftHandAnchor");

        rightHand = rightHandAnchor.AddComponent<WeaponAnchor>();
        if (rightHand == null)
            Debug.LogError("ACharacterWeapons.Start() - couldn't get component of type WeaponAnchor in rightHandAnchor");

        leftHand.SetWeapon(ItemManager.Instance.CreateObject<Shield>());
        rightHand.SetWeapon(ItemManager.Instance.CreateObject<Sword>());
    }

    public void SetController(ACharacterController characterController)
    {
        Animator characterAnimator = characterController.GetComponent<Animator>();
        if (characterAnimator == null)
            Debug.LogError("ACharacterWeapons.SetController() - couldn't get component of type Animator in ACharacterController");

        CharacterSwitchWeapon[] smb = characterAnimator.GetBehaviours<CharacterSwitchWeapon>();
        if (smb == null)
            Debug.LogError("ACharacterWeapons.SetController() - couldn't get behaviours of type CharacterSwitchWeapon in characterAnimator");
        foreach (CharacterSwitchWeapon oneSmb in smb)
            oneSmb.OnSwitch += SwitchWeapon;
    }

    protected void SetWeapons(Item leftWeapon, Item rightWeapon)
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
