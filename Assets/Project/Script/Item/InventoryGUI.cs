using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {

    //Left Panel
    [SerializeField]
    GameObject items_list = null;
    [SerializeField]
    GameObject item_template = null;

    //Right Panel
    [SerializeField]
    Text item_name = null;
    [SerializeField]
    Text item_caracteristics = null;

    private Inventory inventory;
    public Inventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    void Start ()
    {
        inventory = gameObject.AddComponent<Inventory>();
        DisplayInventory<Item>();
	}

    void DisplayInventory<T>() where T : Item
    {
        foreach (Item item in inventory.GetItems<T>())
        {
            AddItem(item);
        }
    }

    void AddItem(Item item)
    {
        GameObject template = Instantiate(item_template);
        template.transform.SetParent(items_list.transform);
        Text name_label = template.transform.FindChild("Name").GetComponent<Text>();
        Text lvl_label = template.transform.FindChild("LVL").GetComponent<Text>();
        Text weight_label = template.transform.FindChild("Weight").GetComponent<Text>();
        Text price_label = template.transform.FindChild("Price").GetComponent<Text>();
        Button button = template.GetComponent<Button>();
        button.onClick.AddListener(delegate { DisplayItem(item); });

        name_label.text = item.NameObject;
        lvl_label.text = item.Level.ToString();
        weight_label.text = item.Weight.ToString();
        price_label.text = item.Price.ToString();
        template.name = item.NameObject;
        ColorItem(template, item);
    }

    public void DisplayItem(Item item)
    {
        if (item != null)
        {
            item_name.text = item.NameObject;
            if(item is IInstanciableItem)
                item_caracteristics.text = ((ITypeItem)item).GetItemInformations();
            else
                item.GetItemGeneralInformations();
        }
    }

    void ColorItem(GameObject template, Item item)
    {
        Image image = template.GetComponent<Image>();
        if (item is Weapon)
            image.color = new Color(0.412f, 0.616f, 0f);
        if (item is Armor)
            image.color = new Color(0.09f, 0.4f, 0.77f);
        else
            image.color = new Color(1f, 0.4f, 0f);
    }
}
