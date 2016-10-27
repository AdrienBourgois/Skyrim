using UnityEngine;

public class Shop : MonoBehaviour, IUsableObject
{
    private InventoryGUI invGui;
    private Inventory inv = new Inventory();

    private void Awake()
    {
        invGui = InventoryGUI.Instance;
        if (!invGui)
            Debug.Log("can't find inventoryGUI on Shop.Awake()");

        invGui.Inventory = inv;
        invGui.current_gui_action = InventoryGUI.Inventory_Gui_Type.VendorInventory;
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.FlagsGeneration.AllType, 10);
    }

    public void OnUse(ACharacter character)
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        invGui.Show = true;
    }
}
