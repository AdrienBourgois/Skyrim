using UnityEngine;
using System.Collections;

public class Fountain : MonoBehaviour , IUsableObject
{

    public void OnUse(ACharacter character)
    {
        character.CharacterStats.BaseCharacteristics.RegenFullHealthAndMana();
    }
}
