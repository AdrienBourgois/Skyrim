using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MagicPanel : MonoBehaviour
{


    List<Spell> printedSpells = new List<Spell>();

    SpellInventory spells;

    GameObject buttonPrefab;

	// Use this for initialization
	void Start ()
    {
        spells = LevelManager.Instance.Player.UnitSpells;
        buttonPrefab = transform.FindChild("SpellSelector").GetChild(0).GetChild(0).GetChild(0).gameObject;
        UpdateSpells();
    }
	
	
	void UpdateSpells ()
    {
        foreach (Spell spell in spells.SpellList)
        {
            if (!printedSpells.Contains(spell))
            {
                printedSpells.Add(spell);
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

        if (spell.Value > 0)
            gao.GetComponent<Image>().color  = Color.green;
        else if (spell.Value == 0)
            gao.GetComponent<Image>().color = Color.cyan;
        else
            gao.GetComponent<Image>().color = Color.magenta;
    }


}
