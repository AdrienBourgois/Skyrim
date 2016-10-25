using UnityEngine;

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
        Inventory inventory = player.UnitInventory;
        invGui.current_gui_action = InventoryGUI.Inventory_Gui_Type.PlayerInventory;
        invGui.Inventory = inventory;
        invGui.Show = true;
    }
}
