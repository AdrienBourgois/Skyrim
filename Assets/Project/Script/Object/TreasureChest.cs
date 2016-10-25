using UnityEngine;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{
    private Animation anim = null;
    private bool hasBeenOpen = false;
    InventoryGUI invGui = null;
    Inventory inv = new Inventory();


    private void Awake()
    {
        Debug.Log("LOLOLOLOL");
        //invGui = ResourceManager.Instance.Load<InventoryGUI>("Gui/InventoryGUI");
        anim = GetComponent<Animation>();

        invGui = InventoryGUI.Instance;
        Debug.Log(invGui);
    }

    void Start()
    {
        Debug.Log("LALALALA");
        invGui.Inventory = inv;
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.flags_generation.All_Type, 10);

        invGui.OnQuitButton.AddListener(delegate { OnUse(null); });
    }



    public void OnUse(ACharacter character)
    {
        if (hasBeenOpen == false)
        {
           
            invGui.current_gui_action = InventoryGUI.Inventory_Gui_Type.ChestInventory;
            
           
            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
            invGui.Show = true;
            
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
