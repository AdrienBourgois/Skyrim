using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    private ACharacter character = null;

    public void SetCharacter(ACharacter _character)
    {
        character = _character;
    }

    public void HitObject(IHitable hitableObject)
    {
        if (character == null)
            return;

        hitableObject.OnHit(character);
    }
}