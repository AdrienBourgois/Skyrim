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

    private SpellProperty spellProp = null;
    private ASpell spell = null;
    
    void Awake()
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

    void Start()
    {        
        leftHand.SetWeapon(ItemManager.Instance.CreateObject<Shield>());
        rightHand.SetWeapon(ItemManager.Instance.CreateObject<Sword>());
    }

    public void SetCharacter(ACharacter character)
    {
        leftHand.SetCharacter(character);
        rightHand.SetCharacter(character);

        character.OnChangedWeapons += SetWeapons;
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


        if (MagicManager.MagicID.NONE < spellProp.Id && spellProp.Id < MagicManager.MagicID.COUNT)
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
