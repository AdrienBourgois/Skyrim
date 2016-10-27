using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InventoryGUI : MonoBehaviour
{

    private static InventoryGUI instance;
    public static InventoryGUI Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = GameObject.FindObjectOfType<InventoryGUI>();
            return instance;
        }
    }

    public enum Inventory_Gui_Type
    {
        PlayerInventory,
        VendorInventory,
        EnemyInventory,
        ChestInventory
    }
    public Inventory_Gui_Type current_gui_action = Inventory_Gui_Type.PlayerInventory;

    //Left Panel
    [SerializeField]
    GameObject items_list;
    [SerializeField]
    GameObject item_panel_template;
    [SerializeField]
    Dropdown filter_list;
    [SerializeField]
    Dropdown sort_list;
    List<GameObject> item_panel_list = new List<GameObject>();

    //Right Panel
    [SerializeField]
    Text item_name;
    [SerializeField]
    Text item_caracteristics;
    [SerializeField]
    Button equip_button;
    [SerializeField]
    Button action_button;
    [SerializeField]
    Button quit_button;
    public Button.ButtonClickedEvent OnQuitButton;

    Item selected_item;

    private Inventory inventory;
    public Inventory Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    private bool is_show;
    public bool Show
    {
        get { return is_show; }
        set
        {
            if (is_show != value)
            {
                is_show = value;
                gameObject.SetActive(is_show);
                if (is_show)
                    ApplyFilterAndSort();
            }
        }
    }

    Dictionary<string, Type> types_conversion = new Dictionary<string, Type>();

    void Awake()
    {
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

        filter_list.onValueChanged.AddListener(delegate { ApplyFilterAndSort(); });
        sort_list.onValueChanged.AddListener(delegate { ApplyFilterAndSort(); });
        OnQuitButton = quit_button.onClick;
        OnQuitButton.AddListener(delegate { Show = false;
                                            FindObjectOfType<IGGui>().gameObject.SetActive(true);
                                            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.InGame); });

        instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void ApplyFilterAndSort()
    {
        Type type = types_conversion[(filter_list.options[filter_list.value].text)];
        typeof(InventoryGUI).GetMethod("DisplayInventory").MakeGenericMethod(type).Invoke(this, null);
    }

    public void DisplayInventory<T>() where T : Item
    {
        int panel_id = 0;
        List<Item> list_to_display = inventory.GetItemsByTypeSorted<T>(sort_list.options[sort_list.value].text);
        ManageItemPanels(list_to_display.Count);
        foreach (Item item in list_to_display)
        {
            FillPanel(item_panel_list[panel_id], item);
            panel_id++;
        }

        Vector2 scroll_rect_position = GetComponentInChildren<ScrollRect>().normalizedPosition;
        scroll_rect_position.y = 1;
        GetComponentInChildren<ScrollRect>().normalizedPosition = scroll_rect_position;
    }

    void ManageItemPanels(int panel_count_needed)
    {
        Vector3 default_scale = new Vector3(1f, 1f, 1f);
        int panel_count_to_generate = panel_count_needed - item_panel_list.Count;
        if (item_panel_list.Count < panel_count_needed)
        {
            for (int i = 0; i < panel_count_to_generate; i++)
            {
                GameObject item_panel = Instantiate(item_panel_template);
                item_panel_list.Add(item_panel);
                item_panel.transform.SetParent(items_list.transform);
                item_panel.transform.localScale = default_scale;
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
        Text value_label = panel.transform.FindChild("Value").GetComponent<Text>();
        Text weight_label = panel.transform.FindChild("Weight").GetComponent<Text>();
        Text price_label = panel.transform.FindChild("Price").GetComponent<Text>();
        Button button = panel.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { DisplayItem(item); });

        name_label.text = item.NameObject;
        lvl_label.text = item.Level.ToString();
        if (item is Armor)
            value_label.text = ((Armor)item).Defense.ToString();
        else if (item is Weapon)
            value_label.text = ((Weapon)item).Damage.ToString();
        else
            value_label.text = "-";
        weight_label.text = item.Weight.ToString();
        price_label.text = item.Price.ToString();
        panel.name = item.NameObject;
        ColorItem(panel, item);
    }

    public void DisplayItem(Item item)
    {
        selected_item = item;
        if (item != null)
        {
            item_name.text = item.NameObject;
            if (item is IInstanciableItem)
                item_caracteristics.text = ((ITypeItem)item).GetItemInformations();
            else
                item.GetItemGeneralInformations();
        }
        else
            ResetSelectedItemGui();

        if (selected_item is IEquipableItem)
        {
            equip_button.GetComponentInChildren<Text>().text = "Equip";
            equip_button.onClick.RemoveAllListeners();
            equip_button.onClick.AddListener(delegate { ((IEquipableItem)item).Equip(); });
            equip_button.interactable = true;
        }
        else if (selected_item is IUseableItem)
        {
            equip_button.GetComponentInChildren<Text>().text = "Use";
            equip_button.onClick.RemoveAllListeners();
            equip_button.onClick.AddListener(delegate { ((IUseableItem)item).Use(); });
            equip_button.interactable = true;
        }
        else
        {
            equip_button.GetComponentInChildren<Text>().text = "";
            equip_button.onClick.RemoveAllListeners();
            equip_button.interactable = false;
        }

        if (current_gui_action == Inventory_Gui_Type.PlayerInventory)
        {
            equip_button.enabled = true;
            equip_button.GetComponentInChildren<Text>().text = "Equip";
            action_button.GetComponentInChildren<Text>().text = "Drop";
            action_button.onClick.RemoveAllListeners();
            action_button.onClick.AddListener(delegate
            {
                FindObjectOfType<Player>().CharacterStats.UnitCharacteristics.PlayerWeight -= selected_item.Weight;
                if (selected_item.Equipped)
                    selected_item.Equipped.RemoveEquipement(selected_item);
                Inventory.RemoveItem(selected_item);
                ApplyFilterAndSort();
                DisplayItem(null);
            });
        }
        else if (current_gui_action == Inventory_Gui_Type.EnemyInventory)
        {
            equip_button.enabled = false;
            equip_button.GetComponentInChildren<Text>().text = "";
            action_button.GetComponentInChildren<Text>().text = "Take";
            action_button.onClick.RemoveAllListeners();
            action_button.onClick.AddListener(delegate
            {
                Player player = FindObjectOfType<Player>();
                if (player.CanCarry(selected_item))
                {
                    player.CharacterStats.UnitCharacteristics.PlayerWeight += selected_item.Weight;
                    inventory.RemoveItem(selected_item);
                    ApplyFilterAndSort();
                    Inventory player_inventory = player.UnitInventory;
                    player_inventory.AddItem(selected_item);
                    DisplayItem(null);
                }
            });
        }
        else if (current_gui_action == Inventory_Gui_Type.VendorInventory)
        {
            equip_button.enabled = false;
            equip_button.GetComponentInChildren<Text>().text = "";
            action_button.GetComponentInChildren<Text>().text = "Buy (Steal)";
            action_button.onClick.RemoveAllListeners();
            action_button.onClick.AddListener(delegate
            {
                Player player = FindObjectOfType<Player>();
                if (player.CanCarry(selected_item))
                {
                    player.CharacterStats.UnitCharacteristics.PlayerWeight += selected_item.Weight;
                    inventory.RemoveItem(selected_item);
                    ApplyFilterAndSort();
                    Inventory player_inventory = player.UnitInventory;
                    player_inventory.AddItem(selected_item);
                    DisplayItem(null);
                }
            });
        }
    }

    void ResetSelectedItemGui()
    {
        item_name.text = "";
        item_caracteristics.text = "";
    }

    void ColorItem(GameObject template, Item item)
    {
        Image image = template.GetComponent<Image>();
        if (item is Weapon)
            image.color = new Color(0.412f, 0.616f, 0f);
        if (item is Armor)
            image.color = new Color(0.09f, 0.6f, 0.9f);
        else
            image.color = new Color(1f, 0.4f, 0f);
    }
}
