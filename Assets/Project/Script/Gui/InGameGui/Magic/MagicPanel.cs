using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MagicPanel : MonoBehaviour
{
    private List<SpellProperty> printedSpells = new List<SpellProperty>();
    private MagicInventory spells;

    private GameObject buttonPrefab;

    public SpellProperty DisplayedMagic { get; private set; }

    private void Start ()
    {
        spells = LevelManager.Instance.Player.UnitSpells;
        buttonPrefab = transform.FindChild("SpellSelector").GetChild(0).GetChild(0).GetChild(0).gameObject;
        UpdateSpells();
    }


    private void UpdateSpells ()
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

    private GameObject CreateBlankButton()
    {
        GameObject gao = Instantiate(buttonPrefab);
        gao.GetComponent<Button>().interactable = true;
        gao.transform.SetParent(buttonPrefab.transform.parent);
        gao.transform.position = buttonPrefab.transform.position;
        gao.transform.localScale = buttonPrefab.transform.localScale;

        return gao;
    }

    private void AddSpellButton(SpellProperty magic)
    {
        GameObject gao = CreateBlankButton();
        gao.name = magic.Id.ToString();
        gao.transform.FindChild("Name").GetComponent<Text>().text = magic.Id.ToString();
        gao.transform.FindChild("Cost").GetComponent<Text>().text = magic.Cost.ToString();

        if (magic.Power != 0 && magic.Id == MagicManager.MagicId.Heal)
            gao.GetComponent<Image>().color  = Color.green;
        else if (magic.Power == 0 )
            gao.GetComponent<Image>().color = Color.cyan;
        else
            gao.GetComponent<Image>().color = Color.magenta;

        gao.GetComponent<Button>().onClick.AddListener(delegate { DisplaySpellButton(magic); });
        printedSpells.Add(magic);
    }

    private void DisplaySpellButton(SpellProperty magic)
    {
        if (magic == null)
            return;

        DisplayedMagic = magic;

        foreach (Transform child in transform.FindChild("Info"))
            child.GetChild(0).GetComponent<Text>().text = magic.GetMemberStringFromString(child.name);

    }

}
