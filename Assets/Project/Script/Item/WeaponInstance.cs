using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    private ACharacter character;

    public void SetCharacter(ACharacter _character)
    {
        character = _character;
    }

    public void HitObject(IHitable _hitableObject)
    {
        if (character == null)
            return;

        _hitableObject.OnHit(character);
    }
}