using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MagicPanel : MonoBehaviour
{
    List<Spell> printedSpells = new List<Spell>();

    SpellInventory spells;

    GameObject buttonPrefab;

	
	void Start ()
    {
        spells = LevelManager.Instance.Player.UnitSpells;
        buttonPrefab = transform.FindChild("SpellSelector").GetChild(0).GetChild(0).GetChild(0).gameObject;
        UpdateSpells();
    }


    private void UpdateSpells ()
    {
        if (spells == null)
            return;

        foreach (Spell spell in spells.SpellList)
        {
            if (!printedSpells.Contains(spell))
            {
                 AddSpellButton(spell);
            }
        }
	}

    GameObject CreateBlankButton()
    {
        GameObject gao = Instantiate(buttonPrefab);
        gao.GetComponent<Button>().interactable = true;
        gao.transform.SetParent(buttonPrefab.transform.parent);
        gao.transform.position = buttonPrefab.transform.position;
        gao.transform.localScale = buttonPrefab.transform.localScale;

        return gao;
    }

    void AddSpellButton(Spell spell)
    {
        GameObject gao = CreateBlankButton();
        gao.name = spell.Name;
        gao.transform.FindChild("Name").GetComponent<Text>().text = spell.Name;
        gao.transform.FindChild("Cost").GetComponent<Text>().text = spell.Cost.ToString();

        if (spell.Power > 0)
            gao.GetComponent<Image>().color  = Color.green;
        else if (spell.Power == 0f)
            gao.GetComponent<Image>().color = Color.cyan;
        else
            gao.GetComponent<Image>().color = Color.magenta;

        gao.GetComponent<Button>().onClick.AddListener(delegate { DisplaySpellButton(spell); });
        printedSpells.Add(spell);
    }

    void DisplaySpellButton(Spell spell)
    {
        if (spell == null)
            return;

        foreach (Transform child in transform.FindChild("Info"))
            child.GetChild(0).GetComponent<Text>().text = spell.GetMemberStringFromString(child.name, true);

    }

}
