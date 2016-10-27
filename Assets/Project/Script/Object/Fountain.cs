using UnityEngine;

public class Fountain : MonoBehaviour , IUsableObject
{

    public void OnUse(ACharacter _character)
    {
        _character.CharacterStats.UnitCharacteristics.RegenFullHealthAndMana();
    }
}
