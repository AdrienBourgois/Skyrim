using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{
    private Animation anim = null;
    private bool hasBeenOpen = false;
    InventoryGUI gui = null;
    Inventory inventory = new Inventory();

    void Awake()
    {
        gui = ResourceManager.Instance.Load<InventoryGUI>("Gui/InventoryGUI");
        anim = GetComponent<Animation>();
        gui = InventoryGUI.Instance;
       
    }

    public void OnUse(ACharacter character)
    {
        if (hasBeenOpen == false)
        {
            gui.Inventory = inventory;
            gui.current_gui_action = InventoryGUI.Inventory_Gui_Type.ChestInventory;
            int score = Random.Range(0, 5);
            inventory.List = ItemManager.Instance.GenerateInventory(ItemManager.flags_generation.All_Type, score);
            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
            gui.Show = true;
            
            anim.Play("open");
            hasBeenOpen = true;
        }
        else
        {
            anim.Play("close");
            hasBeenOpen = false;
        }
    }
}
