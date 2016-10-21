using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour, IUsableObject
{
    InventoryGUI invGui;

    void Awake()
    {
        invGui = InventoryGUI.Instance;
    }

    public void OnUse(ACharacter character)
    {
        Inventory inventory = GetComponent<Inventory>() 
                                ? GetComponent<Inventory>() 
                                : gameObject.AddComponent<Inventory>();

        
        invGui.Inventory = inventory;
        invGui.current_gui_action = InventoryGUI.Inventory_Gui_Type.VendorInventory;

        ItemManager itemMgr = new ItemManager();
        inventory.List = itemMgr.GenerateInventory(ItemManager.flags_generation.All_Type, 50);

        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        invGui.Show = true;
    }
}
