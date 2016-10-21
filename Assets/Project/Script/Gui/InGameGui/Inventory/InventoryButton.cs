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
        Inventory inventory = player.GetComponent<Inventory>();
        invGui.Inventory = inventory;
        invGui.Show = true;
    }
}
