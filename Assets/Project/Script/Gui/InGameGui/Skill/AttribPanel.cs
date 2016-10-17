using UnityEngine;
using UnityEngine.UI;

public class AttribPanel : MonoBehaviour {

    Player player;
    Attributes attrib;
    public int bonusToAssign;

    public void Init()
    {
        if (!player)
        {
            player = LevelManager.Instance.Player;
            attrib = player.CharacterStats.UnitAttributes;
            bonusToAssign = player.AttributePointToAssign;
        }

        UpdateStats();
    }

    void UpdateStats()
    {
        if (!player)
            return;

        foreach (Transform child in transform)
        {
            if (child.GetComponent<AttribGui>())
                child.FindChild("Curr").GetComponent<Text>().text = attrib.GetAttribFromString(child.name).ToString();
        }

        UpdateBonusPoint();
    }

    public void UpdateBonusPoint()
    {
        transform.FindChild("PointLeft").GetChild(0).GetComponent<Text>().text = bonusToAssign.ToString();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<AttribGui>())
                child.GetComponent<AttribGui>().Actualize();
        }
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

        player.AttributePointToAssign = bonusToAssign;
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

        bonusToAssign += bonusToRecover;
        UpdateBonusPoint();
    }
}
