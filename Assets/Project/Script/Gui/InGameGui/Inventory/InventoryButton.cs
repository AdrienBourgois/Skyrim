using UnityEngine;
using System.Collections;

public class InventoryButton : MonoBehaviour
{
    InventoryGUI invGui;
    GameObject pausePanel;

    void Start()
    {
        invGui = InventoryGUI.Instance;
        pausePanel = GameObject.Find("PausePanel");
    }
	
    public void OnClick()
    {
        pausePanel.SetActive(false);
        Inventory inventory = gameObject.AddComponent<Inventory>();

        ItemManager itemMgr = new ItemManager();
        inventory.List = itemMgr.GenerateInventory(ItemManager.flags_generation.All_Type, 50);

        invGui.Inventory = inventory;
        invGui.Show = true;
    }
}
