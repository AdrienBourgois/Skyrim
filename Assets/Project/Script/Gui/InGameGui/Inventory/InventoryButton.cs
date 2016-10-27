using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private InventoryGui invGui;
    private GameObject pausePanel;
    private Player player;

    private void Start()
    {
        player = LevelManager.Instance.Player;
        invGui = InventoryGui.Instance;
        pausePanel = GameObject.Find("PausePanel");
    }
	
    public void OnClick()
    {
        pausePanel.SetActive(false);
        Inventory inventory = player.UnitInventory;
        invGui.currentGuiAction = InventoryGui.InventoryGuiType.PlayerInventory;
        invGui.Inventory = inventory;
        invGui.Show = true;
    }
}
