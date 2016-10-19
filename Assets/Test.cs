using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    InventoryGUI gui = null;

	void Start () {
        gui = InventoryGUI.Instance;
        ItemManager im = new ItemManager();
        Inventory inventory = gameObject.AddComponent<Inventory>();
        inventory.List = im.GenerateInventory(ItemManager.flags_generation.All_Type, 50);
        gui.Inventory = inventory;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
            gui.Show = true;
	}
}
