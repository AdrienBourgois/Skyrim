using UnityEngine;

public class Shop : MonoBehaviour, IUsableObject
{
    private InventoryGui invGui;
    private Inventory inv = new Inventory();

    private void Awake()
    {
        invGui = InventoryGui.Instance;
        if (!invGui)
            Debug.Log("can't find inventoryGUI on Shop.Awake()");

        invGui.Inventory = inv;
        invGui.currentGuiAction = InventoryGui.InventoryGuiType.VendorInventory;
    }

    void Start()
    {
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.FlagsGeneration.AllType, 10);
    }

    public void OnUse(ACharacter character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        invGui.Show = true;
    }
}
