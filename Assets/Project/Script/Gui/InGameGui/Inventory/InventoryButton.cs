using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    private InventoryPanelGui invPanelGui;
    private GameObject pausePanel;
    private Player player;

    private void Start()
    {
        player = LevelManager.Instance.Player;
        invPanelGui = InventoryPanelGui.Instance;
        pausePanel = GameObject.Find("PausePanel");
    }

    public void OnClick()
    {
        pausePanel.SetActive(false);
        Inventory inventory = player.UnitInventory;
        invPanelGui.currentGuiAction = InventoryPanelGui.InventoryGuiType.PlayerInventory;
        invPanelGui.Inventory = inventory;
        invPanelGui.Show = true;
    }
}
