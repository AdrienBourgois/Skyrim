using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryGUI : MonoBehaviour {

    //Left Panel
    [SerializeField]
    GameObject items_list = null;
    [SerializeField]
    GameObject item_panel_template = null;
    [SerializeField]
    Dropdown filter_list = null;
    public List<GameObject> item_panel_list = new List<GameObject>();

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

    Dictionary<string, System.Type> types_conversion = new Dictionary<string, System.Type>();

    void Start ()
    {
        inventory = gameObject.AddComponent<Inventory>();
        DisplayInventory<Item>();

        types_conversion.Add("<color=olive><b> -> All <- </b></color>", typeof(Item));
        types_conversion.Add("<color=teal><b>---- Armor ----</b></color>", typeof(Armor));
        types_conversion.Add("<color=teal>Boots</color>", typeof(Boots));
        types_conversion.Add("<color=teal>Shield</color>", typeof(Shield));
        types_conversion.Add("<color=teal>Torso</color>", typeof(Torso));
        types_conversion.Add("<color=teal>Helmet</color>", typeof(Helmet));
        types_conversion.Add("<color=orange><b>---- Weapon ----</b></color>", typeof(Weapon));
        types_conversion.Add("<color=orange>Sword</color>", typeof(Sword));
        types_conversion.Add("<color=orange>Axe</color>", typeof(Axe));
        filter_list.ClearOptions();
        filter_list.AddOptions(new List<string>(types_conversion.Keys));
        filter_list.onValueChanged.AddListener(delegate {
            System.Type type = types_conversion[(filter_list.options[filter_list.value].text)];
            typeof(InventoryGUI).GetMethod("DisplayInventory").MakeGenericMethod(new[] { type }).Invoke(this, null);
        });
    }

    public void ResetInventory()
    {
        inventory = gameObject.AddComponent<Inventory>();
        DisplayInventory<Item>();
    }

    public void DisplayInventory<T>() where T : Item
    {
        System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

        int panel_id = 0;
        List<Item> list_to_display = inventory.GetItems<T>();
        ManageItemPanels(list_to_display.Count);
        foreach (Item item in list_to_display)
        {
            FillPanel(item_panel_list[panel_id], item);
            panel_id++;
        }

        watch.Stop();
        Debug.Log("Inventory displayed in " + watch.ElapsedMilliseconds + " ms");
    }

    void ManageItemPanels(int panel_count_needed)
    {
        Debug.Log(panel_count_needed);
        int panel_count_to_generate = panel_count_needed - item_panel_list.Count;
        if (item_panel_list.Count < panel_count_needed)
        {
            for (int i = 0; i < panel_count_to_generate; i++)
            {
                GameObject item_panel = Instantiate(item_panel_template);
                item_panel_list.Add(item_panel);
                item_panel.transform.SetParent(items_list.transform);
            }
        }
        for (int i = 0; i < panel_count_needed; i++)
            item_panel_list[i].SetActive(true);
        for (int i = panel_count_needed; i < item_panel_list.Count; i++)
            item_panel_list[i].SetActive(false);
    }

    void FillPanel(GameObject panel, Item item)
    {
        Text name_label = panel.transform.FindChild("Name").GetComponent<Text>();
        Text lvl_label = panel.transform.FindChild("LVL").GetComponent<Text>();
        Text weight_label = panel.transform.FindChild("Weight").GetComponent<Text>();
        Text price_label = panel.transform.FindChild("Price").GetComponent<Text>();
        Button button = panel.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { DisplayItem(item); });

        name_label.text = item.NameObject;
        lvl_label.text = item.Level.ToString();
        weight_label.text = item.Weight.ToString();
        price_label.text = item.Price.ToString();
        panel.name = item.NameObject;
        ColorItem(panel, item);
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
