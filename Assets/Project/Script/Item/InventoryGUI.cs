﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InventoryGUI : MonoBehaviour {

    private static InventoryGUI instance = null;
    public static InventoryGUI Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = GameObject.Find("InventoryGUI").GetComponent<InventoryGUI>();
            return instance;
        }
    }

    public enum Inventory_Gui_Type
    {
        PlayerInventory,
        VendorInventory,
        EnemyInventory,
    }
    Inventory_Gui_Type current_gui_action = Inventory_Gui_Type.PlayerInventory;

    //Left Panel
    [SerializeField]
    GameObject items_list = null;
    [SerializeField]
    GameObject item_panel_template = null;
    [SerializeField]
    Dropdown filter_list = null;
    [SerializeField]
    Dropdown sort_list = null;
    List<GameObject> item_panel_list = new List<GameObject>();

    //Right Panel
    [SerializeField]
    Text item_name = null;
    [SerializeField]
    Text item_caracteristics = null;
    [SerializeField]
    Button equip_button = null;
    [SerializeField]
    Button action_button = null;
    [SerializeField]
    Button quit_button = null;

    Item selected_item = null;

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
        quit_button.onClick.AddListener(delegate { Show = false; });
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void ApplyFilterAndSort()
    {
        Type type = types_conversion[(filter_list.options[filter_list.value].text)];
        typeof(InventoryGUI).GetMethod("DisplayInventory").MakeGenericMethod(new[] { type }).Invoke(this, null);
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
            if(item is IInstanciableItem)
                item_caracteristics.text = ((ITypeItem)item).GetItemInformations();
            else
                item.GetItemGeneralInformations();
        }

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
            action_button.GetComponentInChildren<Text>().text = "Drop";
            action_button.onClick.RemoveAllListeners();
            action_button.onClick.AddListener(delegate {
                Inventory.RemoveItem(selected_item);
                ApplyFilterAndSort();
            });
        }
        else if (current_gui_action == Inventory_Gui_Type.EnemyInventory)
        {
            action_button.GetComponentInChildren<Text>().text = "Take";
            action_button.onClick.RemoveAllListeners();
            action_button.onClick.AddListener(delegate {
                inventory.RemoveItem(selected_item);
                ApplyFilterAndSort();
                throw new NotImplementedException("Fix when an inventory is attach to player !");
                /*Inventory player_inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
                player_inventory.AddItem(selected_item);*/
            });
        }
        else if (current_gui_action == Inventory_Gui_Type.EnemyInventory)
        {
            action_button.GetComponentInChildren<Text>().text = "Take";
            action_button.onClick.RemoveAllListeners();
            action_button.onClick.AddListener(delegate
            {
                inventory.RemoveItem(selected_item);
                ApplyFilterAndSort();
                throw new NotImplementedException("Fix when an inventory is attach to player !");
            });
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