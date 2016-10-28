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

    private void Awake()
    {
        if (leftHandAnchor == null || rightHandAnchor == null)
            Debug.LogError("ACharacterWeapons.Start() - leftHandAnchor and rightHandAnchor should be initialized.");

        leftHand = leftHandAnchor.AddComponent<WeaponAnchor>();
        if (leftHand == null)
            Debug.LogError("ACharacterWeapons.Start() - couldn't get component of type WeaponAnchor in leftHandAnchor");

        rightHand = rightHandAnchor.AddComponent<WeaponAnchor>();
        if (rightHand == null)
            Debug.LogError("ACharacterWeapons.Start() - couldn't get component of type WeaponAnchor in rightHandAnchor");
    }

    public void SetCharacter(ACharacter _character)
    {
        leftHand.SetCharacter(_character);
        rightHand.SetCharacter(_character);

        _character.OnChangedWeapons += SetWeapons;

        SetWeapons(ItemManager.Instance.CreateObject<Shield>(), ItemManager.Instance.CreateObject<Sword>());
    }

    public void SetController(ACharacterController _characterController)
    {
        controller = _characterController;

        Animator characterAnimator = _characterController.GetComponent<Animator>();
        if (characterAnimator == null)
            Debug.LogError("ACharacterWeapons.SetController() - couldn't get component of type Animator in ACharacterController");

        CharacterSwitchWeapon[] characterSwitchBehaviour = characterAnimator.GetBehaviours<CharacterSwitchWeapon>();
        if (characterSwitchBehaviour == null)
            Debug.LogError("ACharacterWeapons.SetController() - couldn't get behaviours of type CharacterSwitchWeapon in characterAnimator");
        foreach (CharacterSwitchWeapon oneCharSwitchBehaviour in characterSwitchBehaviour)
            oneCharSwitchBehaviour.OnSwitch += SwitchWeapon;
    }

    private void SetWeapons(Item _leftWeapon, Item _rightWeapon)
    {
        leftHand.SetWeapon(_leftWeapon);
        rightHand.SetWeapon(_rightWeapon);
    }

    private void SwitchWeapon()
    {
        leftHand.Switch();
        rightHand.Switch();
    }

    public bool InstanciateMagic()
    {
        if (spellProp == null || spell != null
            || controller.Character.CharacterStats.UnitCharacteristics.Mana < spellProp.Cost)
                return false;


        if (MagicManager.MagicId.None < spellProp.Id && spellProp.Id < MagicManager.MagicId.Count)
        {
            controller.Character.CharacterStats.UnitCharacteristics.Mana -= spellProp.Cost;

            spell = MagicManager.Instance.CreateSpell(spellProp, controller);
            spell.gameObject.transform.parent = rightHandAnchor.transform;
            spell.gameObject.transform.localPosition = Vector3.zero;
            return true;
        }
        return false;
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

    public void SetActiveMagic(SpellProperty _spellProp)
    {
        spellProp = _spellProp;
    }
}
