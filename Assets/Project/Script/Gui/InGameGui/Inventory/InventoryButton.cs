using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private InventoryGUI invGui;
    private GameObject pausePanel;
    private Player player;

    private void Start()
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
