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


    }

    public void OnUse(ACharacter character)
    {
        invGui.Inventory = inv;
        invGui.current_gui_action = InventoryGUI.Inventory_Gui_Type.VendorInventory;
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.flags_generation.All_Type, 50);

        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        invGui.Show = true;
    }
}
