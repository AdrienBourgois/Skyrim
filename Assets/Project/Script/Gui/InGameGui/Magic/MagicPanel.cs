using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MagicPanel : MonoBehaviour
{
    List<SpellProperty> printedSpells = new List<SpellProperty>();
    MagicInventory spells;

    GameObject buttonPrefab;

    SpellProperty displayedMagic = null;
	public SpellProperty DisplayedMagic
    { get { return displayedMagic; } }

	void Start ()
    {
        spells = LevelManager.Instance.Player.UnitSpells;
        buttonPrefab = transform.FindChild("SpellSelector").GetChild(0).GetChild(0).GetChild(0).gameObject;
        UpdateSpells();
    }
	
	
	public void UpdateSpells ()
    {
        if (spells == null)
            return;

        foreach (SpellProperty spell in spells.MagicList)
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

    void AddSpellButton(SpellProperty magic)
    {
        GameObject gao = CreateBlankButton();
        gao.name = magic.MagicName;
        gao.transform.FindChild("Name").GetComponent<Text>().text = magic.MagicName;
        gao.transform.FindChild("Cost").GetComponent<Text>().text = magic.Cost.ToString();

        if (magic.Power != 0 && magic.ID == MagicManager.MagicID.Heal)
            gao.GetComponent<Image>().color  = Color.green;
        else if (magic.Power == 0 )
            gao.GetComponent<Image>().color = Color.cyan;
        else
            gao.GetComponent<Image>().color = Color.magenta;

        gao.GetComponent<Button>().onClick.AddListener(delegate { DisplaySpellButton(magic); });
        printedSpells.Add(magic);
    }

    void DisplaySpellButton(SpellProperty magic)
    {
        if (magic == null)
            return;

        displayedMagic = magic;

        foreach (Transform child in transform.FindChild("Info"))
            child.GetChild(0).GetComponent<Text>().text = magic.GetMemberStringFromString(child.name);

    }

}
