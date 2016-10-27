using UnityEngine;

public class Fountain : MonoBehaviour , IUsableObject
{

    public void OnUse(ACharacter character)
    {
        character.CharacterStats.UnitCharacteristics.RegenFullHealthAndMana();
    }
}
