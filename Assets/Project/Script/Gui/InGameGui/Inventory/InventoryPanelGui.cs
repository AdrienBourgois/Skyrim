using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InventoryPanelGui : MonoBehaviour
{

    private static InventoryPanelGui instance;
    public static InventoryPanelGui Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<InventoryPanelGui>();
            return instance;
        }
    }

    public enum InventoryGuiType
    {
        PlayerInventory,
        VendorInventory,
        EnemyInventory,
        ChestInventory
    }
    public InventoryGuiType currentGuiAction = InventoryGuiType.PlayerInventory;

    //Left Panel
    [SerializeField] private GameObject itemsList;
    [SerializeField] private GameObject itemPanelTemplate;
    [SerializeField] private Dropdown filterList;
    [SerializeField] private Dropdown sortList;
    private List<GameObject> itemPanelList = new List<GameObject>();

    //Right Panel
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemCaracteristics;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button actionButton;
    [SerializeField] private Button quitButton;
    public Button.ButtonClickedEvent onQuitButton;

    private Item selectedItem;

    public Inventory Inventory { get; set; }

    private bool isShow;
    public bool Show
    {
        get { return isShow; }
        set
        {
            if (isShow != value)
            {
                isShow = value;
                gameObject.SetActive(isShow);
                if (isShow)
                    ApplyFilterAndSort();
            }
        }
    }

    private Dictionary<string, Type> typesConversion = new Dictionary<string, Type>();

    private void Awake()
    {
        typesConversion.Add("<color=olive><b> -> All <- </b></color>", typeof(Item));
        typesConversion.Add("<color=teal><b>---- Armor ----</b></color>", typeof(Armor));
        typesConversion.Add("<color=teal>Boots</color>", typeof(Boots));
        typesConversion.Add("<color=teal>Shield</color>", typeof(Shield));
        typesConversion.Add("<color=teal>Torso</color>", typeof(Torso));
        typesConversion.Add("<color=teal>Helmet</color>", typeof(Helmet));
        typesConversion.Add("<color=orange><b>---- Weapon ----</b></color>", typeof(Weapon));
        typesConversion.Add("<color=orange>Sword</color>", typeof(Sword));
        typesConversion.Add("<color=orange>Axe</color>", typeof(Axe));
        filterList.ClearOptions();
        filterList.AddOptions(new List<string>(typesConversion.Keys));

        filterList.onValueChanged.AddListener(delegate { ApplyFilterAndSort(); });
        sortList.onValueChanged.AddListener(delegate { ApplyFilterAndSort(); });
        onQuitButton = quitButton.onClick;
        onQuitButton.AddListener(delegate { Show = false;
                                            FindObjectOfType<IGGui>().gameObject.SetActive(true);
                                            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.InGame); });

        instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void ApplyFilterAndSort()
    {
        Type type = typesConversion[filterList.options[filterList.value].text];
        typeof(InventoryPanelGui).GetMethod("DisplayInventory").MakeGenericMethod(type).Invoke(this, null);
    }

    public void DisplayInventory<T>() where T : Item
    {
        int panelId = 0;
        List<Item> listToDisplay = Inventory.GetItemsByTypeSorted<T>(sortList.options[sortList.value].text);
        ManageItemPanels(listToDisplay.Count);
        foreach (Item item in listToDisplay)
        {
            FillPanel(itemPanelList[panelId], item);
            panelId++;
        }

        Vector2 scrollRectPosition = GetComponentInChildren<ScrollRect>().normalizedPosition;
        scrollRectPosition.y = 1;
        GetComponentInChildren<ScrollRect>().normalizedPosition = scrollRectPosition;
    }

    private void ManageItemPanels(int _panelCountNeeded)
    {
        Vector3 defaultScale = new Vector3(1f, 1f, 1f);
        int panelCountToGenerate = _panelCountNeeded - itemPanelList.Count;
        if (itemPanelList.Count < _panelCountNeeded)
        {
            for (int i = 0; i < panelCountToGenerate; i++)
            {
                GameObject itemPanel = Instantiate(itemPanelTemplate);
                itemPanelList.Add(itemPanel);
                itemPanel.transform.SetParent(itemsList.transform);
                itemPanel.transform.localScale = defaultScale;
            }
        }
        for (int i = 0; i < _panelCountNeeded; i++)
            itemPanelList[i].SetActive(true);
        for (int i = _panelCountNeeded; i < itemPanelList.Count; i++)
            itemPanelList[i].SetActive(false);
    }

    private void FillPanel(GameObject _panel, Item _item)
    {
        Text nameLabel = _panel.transform.FindChild("Name").GetComponent<Text>();
        Text lvlLabel = _panel.transform.FindChild("LVL").GetComponent<Text>();
        Text valueLabel = _panel.transform.FindChild("Value").GetComponent<Text>();
        Text weightLabel = _panel.transform.FindChild("Weight").GetComponent<Text>();
        Text priceLabel = _panel.transform.FindChild("Price").GetComponent<Text>();
        Button button = _panel.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { DisplayItem(_item); });

        nameLabel.text = _item.NameObject;
        lvlLabel.text = _item.Level.ToString();
        if (_item is Armor)
            valueLabel.text = ((Armor)_item).Defense.ToString();
        else if (_item is Weapon)
            valueLabel.text = ((Weapon)_item).Damage.ToString();
        else
            valueLabel.text = "-";
        weightLabel.text = _item.Weight.ToString();
        priceLabel.text = _item.Price.ToString();
        _panel.name = _item.NameObject;
        ColorItem(_panel, _item);
    }

    public void DisplayItem(Item _item)
    {
        selectedItem = _item;
        if (_item != null)
        {
            itemName.text = _item.NameObject;
            if (_item is IInstanciableItem)
                itemCaracteristics.text = ((ITypeItem)_item).GetItemInformations();
            else
                _item.GetItemGeneralInformations();
        }
        else
            ResetSelectedItemGui();

        if (selectedItem is IEquipableItem)
        {
            equipButton.GetComponentInChildren<Text>().text = "Equip";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(delegate { ((IEquipableItem)_item).Equip(); });
            equipButton.interactable = true;
        }
        else if (selectedItem is IUseableItem)
        {
            equipButton.GetComponentInChildren<Text>().text = "Use";
            equipButton.onClick.RemoveAllListeners();
            equipButton.onClick.AddListener(delegate { ((IUseableItem)_item).Use(); });
            equipButton.interactable = true;
        }
        else
        {
            equipButton.GetComponentInChildren<Text>().text = "";
            equipButton.onClick.RemoveAllListeners();
            equipButton.interactable = false;
        }

        if (currentGuiAction == InventoryGuiType.PlayerInventory)
        {
            equipButton.enabled = true;
            equipButton.GetComponentInChildren<Text>().text = "Equip";
            actionButton.GetComponentInChildren<Text>().text = "Drop";
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(delegate
            {
                FindObjectOfType<Player>().CharacterStats.UnitCharacteristics.PlayerWeight -= selectedItem.Weight;
                if (selectedItem.Equipped)
                    selectedItem.Equipped.RemoveEquipement(selectedItem);
                Inventory.RemoveItem(selectedItem);
                ApplyFilterAndSort();
                DisplayItem(null);
            });
        }
        else if (currentGuiAction == InventoryGuiType.EnemyInventory)
        {
            equipButton.enabled = false;
            equipButton.GetComponentInChildren<Text>().text = "";
            actionButton.GetComponentInChildren<Text>().text = "Take";
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(delegate
            {
                Player player = FindObjectOfType<Player>();
                if (player.CanCarry(selectedItem))
                {
                    player.CharacterStats.UnitCharacteristics.PlayerWeight += selectedItem.Weight;
                    Inventory.RemoveItem(selectedItem);
                    ApplyFilterAndSort();
                    Inventory player_inventory = player.UnitInventory;
                    player_inventory.AddItem(selectedItem);
                    DisplayItem(null);
                }
            });
        }
        else if (currentGuiAction == InventoryGuiType.VendorInventory)
        {
            equipButton.enabled = false;
            equipButton.GetComponentInChildren<Text>().text = "";
            actionButton.GetComponentInChildren<Text>().text = "Buy (Steal)";
            actionButton.onClick.RemoveAllListeners();
            actionButton.onClick.AddListener(delegate
            {
                Player player = FindObjectOfType<Player>();
                if (player.CanCarry(selectedItem))
                {
                    player.CharacterStats.UnitCharacteristics.PlayerWeight += selectedItem.Weight;
                    Inventory.RemoveItem(selectedItem);
                    ApplyFilterAndSort();
                    Inventory player_inventory = player.UnitInventory;
                    player_inventory.AddItem(selectedItem);
                    DisplayItem(null);
                }
            });
        }
    }

    private void ResetSelectedItemGui()
    {
        itemName.text = "";
        itemCaracteristics.text = "";
    }

    private void ColorItem(GameObject _template, Item _item)
    {
        Image image = _template.GetComponent<Image>();
        if (_item is Weapon)
            image.color = new Color(0.412f, 0.616f, 0f);
        if (_item is Armor)
            image.color = new Color(0.09f, 0.6f, 0.9f);
        else
            image.color = new Color(1f, 0.4f, 0f);
    }
}
