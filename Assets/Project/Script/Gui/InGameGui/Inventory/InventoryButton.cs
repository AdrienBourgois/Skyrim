using UnityEngine;
using System.Collections;

public class InventoryButton : MonoBehaviour
{
    InventoryGUI invGui;
    GameObject pausePanel;
    Player player;

    void Start()
    {
        player = LevelManager.Instance.Player;
        invGui = InventoryGUI.Instance;
        pausePanel = GameObject.Find("PausePanel");
    }
	
    public void OnClick()
    {
        pausePanel.SetActive(false);
        Inventory inventory;

        if (!player.GetComponent<Inventory>())
        {
            inventory = player.gameObject.AddComponent<Inventory>();

            ItemManager itemMgr = new ItemManager();
            inventory.List = itemMgr.GenerateInventory(ItemManager.flags_generation.All_Type, 50);
        }
        else
            inventory = player.GetComponent<Inventory>();

        invGui.Inventory = inventory;
        invGui.Show = true;
    }
}
