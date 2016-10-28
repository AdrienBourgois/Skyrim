using UnityEngine;
using UnityEngine.UI;

public class AttribGui : MonoBehaviour {

    public void ResetBonus()
    {
        transform.FindChild("Bonus").GetComponent<Text>().text = "0";
    }

    public void Actualize()
    {
        int bonusToAssign = transform.parent.GetComponent<AttribPanel>().BonusToAssign;
        transform.FindChild("Plus").GetComponent<Button>().enabled = bonusToAssign > 0;

        int bonusAssigned = int.Parse(transform.FindChild("Bonus").GetComponent<Text>().text);
        transform.FindChild("Minus").GetComponent<Button>().enabled = bonusAssigned > 0;
    }

    [Useless]
    public void Plus()
    {
        transform.FindChild("Bonus").GetComponent<Text>().text = (int.Parse(transform.GetChild(1).GetComponent<Text>().text) + 1).ToString();
        transform.parent.GetComponent<AttribPanel>().BonusToAssign--;
        transform.parent.GetComponent<AttribPanel>().UpdateBonusPoint();
        Actualize();
    }

    [Useless]
    public void Minus()
    {
        transform.FindChild("Bonus").GetComponent<Text>().text = (int.Parse(transform.GetChild(1).GetComponent<Text>().text) - 1).ToString();
        transform.parent.GetComponent<AttribPanel>().BonusToAssign++;
        transform.parent.GetComponent<AttribPanel>().UpdateBonusPoint();
        Actualize();
    }
}
