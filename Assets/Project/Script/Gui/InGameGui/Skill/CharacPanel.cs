using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacPanel : MonoBehaviour
{
    Player player;
    Characteristics charac;

    void Start()
    {
        player = LevelManager.Instance.Player;
        charac = player.CharacterStats.UnitCharacteristics;
    }

    public void UpdateStats()
    {
        player.CharacterStats.SetCharacteristics(player);
       
        foreach (Transform child in transform)
            if (child.name != "Charac")
                child.FindChild("Curr").GetComponent<Text>().text = charac.GetCharacFromString(child.name).ToString();
    }

    public void UpdateBonus()
    {
        Attributes attrib = player.CharacterStats.UnitAttributes;
        Dictionary<string, int> bonusAttribDic = transform.parent.GetComponentInChildren<AttribPanel>().GetBonusAttrib();
        Dictionary<string, float> SimulDic = player.CharacterStats.SimulateCharac(player.UnitLevel,
                                                                                  player.CharacterStats.UnitCharacteristics.Weight,
                                                                                  attrib.Strength + bonusAttribDic["Strength"],
                                                                                  attrib.Constitution + bonusAttribDic["Constitution"],
                                                                                  attrib.Intelligence + bonusAttribDic["Intelligence"],
                                                                                  attrib.Dexterity + bonusAttribDic["Dexterity"]);
        foreach (Transform child in transform)
        {
            if (child.name != "Charac")
            {
                float bonus = SimulDic[child.name] - charac.GetCharacFromString(child.name);
                bonus = bonus > 0 ? bonus : 0;
                child.FindChild("Bonus").GetComponent<Text>().text = bonus.ToString();
            }
        }
    }

    public void Validate()
    {
        player.CharacterStats.SetCharacteristics(player);
        UpdateStats();
        Reset();
    }

    public void Reset()
    {
        foreach (Transform child in transform)
            if (child.name != "Charac")
                child.FindChild("Bonus").GetComponent<Text>().text = "0";
    }

}
