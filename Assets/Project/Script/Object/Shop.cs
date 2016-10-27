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


    }

    public void OnUse(ACharacter character)
    {
        invGui.Inventory = inv;
        invGui.currentGuiAction = InventoryGui.InventoryGuiType.VendorInventory;
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.FlagsGeneration.AllType, 50);

        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
        invGui.Show = true;
    }
}
