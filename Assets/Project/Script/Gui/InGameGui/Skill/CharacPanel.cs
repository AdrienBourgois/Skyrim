using UnityEngine;
using System.Collections;

public class CharacPanel : MonoBehaviour
{
    Characteristics charac;

    public void Init()
    {
        charac = LevelManager.Instance.Player.CharacterStats.UnitCharacteristics;
    }
	
}
