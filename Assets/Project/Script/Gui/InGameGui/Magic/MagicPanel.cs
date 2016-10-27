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

    private void AddSpellButton(SpellProperty _magic)
    {
        GameObject gao = CreateBlankButton();
        gao.name = _magic.Id.ToString();
        gao.transform.FindChild("Name").GetComponent<Text>().text = _magic.Id.ToString();
        gao.transform.FindChild("Cost").GetComponent<Text>().text = _magic.Cost.ToString();

        if (_magic.Power != 0 && _magic.Id == MagicManager.MagicId.Heal)
            gao.GetComponent<Image>().color  = Color.green;
        else if (_magic.Power == 0 )
            gao.GetComponent<Image>().color = Color.cyan;
        else
            gao.GetComponent<Image>().color = Color.magenta;

        gao.GetComponent<Button>().onClick.AddListener(delegate { DisplaySpellButton(_magic); });
        printedSpells.Add(_magic);
    }

    private void DisplaySpellButton(SpellProperty _magic)
    {
        if (_magic == null)
            return;

        DisplayedMagic = _magic;

        foreach (Transform child in transform.FindChild("Info"))
            child.GetChild(0).GetComponent<Text>().text = _magic.GetMemberStringFromString(child.name);

    }

}
