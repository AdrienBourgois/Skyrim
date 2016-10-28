using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacPanel : MonoBehaviour
{
    private Player player;
    private Characteristics charac;

    private void Start()
    {
        player = LevelManager.Instance.Player;
        charac = player.CharacterStats.UnitCharacteristics;
        UpdateStats();
    }

    private void UpdateStats()
    {
        if (!player)
            return;

        player.CharacterStats.SetCharacteristics(player);

        foreach (Transform child in transform)
            if (child.name != "Charac")
            {
                string rounding = Round(child.name);
                child.FindChild("Curr").GetComponent<Text>().text = charac.GetCharacFromString(child.name).ToString(rounding);
            }
    }


    public void UpdateBonus()
    {
        Attributes attrib = player.CharacterStats.UnitAttributes;
        Dictionary<string, int> bonusAttribDic = transform.parent.GetComponentInChildren<AttribPanel>().GetBonusAttrib();
        Characteristics simulCharac = player.CharacterStats.SimulateCharac(player.UnitLevel,
                                                                                  player.CharacterStats.UnitCharacteristics.PlayerWeight,
                                                                                  attrib.Strength + bonusAttribDic["Strength"],
                                                                                  attrib.Constitution + bonusAttribDic["Constitution"],
                                                                                  attrib.Intelligence + bonusAttribDic["Intelligence"],
                                                                                  attrib.Dexterity + bonusAttribDic["Dexterity"]);



        foreach (Transform child in transform)
            if (child.name != "Charac")
            {
                float bonus = simulCharac.GetCharacFromString(child.name) - charac.GetCharacFromString(child.name);
                bonus = bonus > 0 ? bonus : 0;
                string rounding = Round(child.name, bonus);
                child.FindChild("Bonus").GetComponent<Text>().text = bonus.ToString(rounding);
            }
    }

    public void Validate()
    {
        UpdateStats();
        Reset();
    }

    private void Reset()
    {
        foreach (Transform child in transform)
            if (child.name != "Charac")
                child.FindChild("Bonus").GetComponent<Text>().text = "0";
    }


    private string Round(string _characName, float _value = 1f)
    {
        return (_characName == "AttackSpeed"
                || _characName == "SpellPower"
                || _characName == "HealthRegeneration")
                && _value != 0f
                ? "0.00" : "0";
    }
}
