using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttribPanel : MonoBehaviour {
    private Player player;
    private Attributes attrib;
    public int BonusToAssign { get; set; }

    private void Start()
    {
        player = LevelManager.Instance.Player;
        attrib = player.CharacterStats.UnitAttributes;
        InitBonusToAssign();
        UpdateStats();
    }

    public void InitBonusToAssign()
    {
        if ( player != null)
            BonusToAssign = player.AttributePointToAssign;
    }

    public void UpdateStats()
    {
        if (attrib == null)
            return;

        attrib.UpdateAttribDict();

        foreach (Transform child in transform)
            if (child.GetComponent<AttribGui>())
                child.FindChild("Curr").GetComponent<Text>().text = attrib.GetAttribFromString(child.name).ToString();
        
        UpdateBonusPoint();
    }

    public void UpdateBonusPoint()
    {
        transform.FindChild("PointLeft").GetChild(0).GetComponent<Text>().text = BonusToAssign.ToString();

        foreach (Transform child in transform)
            if (child.GetComponent<AttribGui>())
                child.GetComponent<AttribGui>().Actualize();
    }

    public void Validate()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<AttribGui>())
            {
                int current = attrib.GetAttribFromString(child.name);
                int bonus = int.Parse(child.FindChild("Bonus").GetComponent<Text>().text);

                attrib.SetAttribFromString(child.name, current + bonus);
                child.GetComponent<AttribGui>().ResetBonus();
            }
        }

        player.CharacterStats.SetCharacteristics(player);

        if (player.AttributePointToAssign != BonusToAssign)
            player.CharacterStats.UnitCharacteristics.RegenFullHealthAndMana();

        player.AttributePointToAssign = BonusToAssign;
        UpdateStats();
    }

    public void Reset()
    {
        int bonusToRecover = 0;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<AttribGui>())
            {
                Text bonusText = child.transform.FindChild("Bonus").GetComponent<Text>();
                bonusToRecover += int.Parse(bonusText.text);
                child.GetComponent<AttribGui>().ResetBonus();
            }
        }

        BonusToAssign += bonusToRecover;
        UpdateBonusPoint();
    }

    public Dictionary<string, int> GetBonusAttrib()
    {
        Dictionary<string, int> bonusAttribDic = new Dictionary<string, int>();


        foreach (Transform child in transform)
            if (child.GetComponent<AttribGui>())
                bonusAttribDic[child.name] = int.Parse(child.FindChild("Bonus").GetComponent<Text>().text);

        return bonusAttribDic;
    }
}
