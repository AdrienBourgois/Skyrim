using UnityEngine;

public class ACharacterWeapons : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private GameObject leftHandAnchor;

    [SerializeField]
    private GameObject rightHandAnchor;
    #endregion

    private ACharacterController controller;

    private WeaponAnchor leftHand;
    private WeaponAnchor rightHand;

    private SpellProperty spellProp;
    private ASpell spell;

    
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

    public void SetCharacter(ACharacter _character)
    {
        _character.OnChangedWeapons += SetWeapons;
    }

    public void SetController(ACharacterController characterController)
    {
        controller = characterController;

        Animator characterAnimator = characterController.GetComponent<Animator>();
        if (characterAnimator == null)
            Debug.LogError("ACharacterWeapons.SetController() - couldn't get component of type Animator in ACharacterController");

        CharacterSwitchWeapon[] characterSwitchBehaviour = characterAnimator.GetBehaviours<CharacterSwitchWeapon>();
        if (characterSwitchBehaviour == null)
            Debug.LogError("ACharacterWeapons.SetController() - couldn't get behaviours of type CharacterSwitchWeapon in characterAnimator");
        foreach (CharacterSwitchWeapon oneCharSwitchBehaviour in characterSwitchBehaviour)
            oneCharSwitchBehaviour.OnSwitch += SwitchWeapon;
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

    public void InstanciateMagic()
    {

        if (spellProp == null || spell != null
            || controller.Character.CharacterStats.UnitCharacteristics.Mana < spellProp.Cost)
                return;


        if (MagicManager.MagicID.NONE < spellProp.Id && spellProp.Id < MagicManager.MagicID.COUNT)
        {
            controller.Character.CharacterStats.UnitCharacteristics.Mana -= spellProp.Cost;

            spell = MagicManager.Instance.CreateSpell(spellProp, controller);
            spell.gameObject.transform.parent = rightHandAnchor.transform;
            spell.gameObject.transform.localPosition = Vector3.zero;
        }
    }

    public void ActivateMagic()
    {
        if (spell != null)
        {
            spell.Activate();
            spell = null;
        }
        else
            Debug.LogWarning("ACharacterWeapon.ActivateMagic() - member \"magic\" is null");
    }

    public void SetActiveMagic(SpellProperty spellProp)
    {
        this.spellProp = spellProp;
    }
}
