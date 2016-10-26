using UnityEngine;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{
    private Animation anim = null;
    private bool hasBeenOpen = false;
    InventoryGUI invGui = null;
    Inventory inv = new Inventory();
    UnityEngine.Events.UnityAction OnCloseChest = null;


    private void Awake()
    {
        anim = GetComponent<Animation>();

        invGui = InventoryGUI.Instance;
    }

    void Start()
    {
        
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.flags_generation.All_Type, 10);

        OnCloseChest = delegate { CloseChest(); };

    }



    public void OnUse(ACharacter character)
    {
        if (hasBeenOpen == false)
        {
            invGui.Inventory = inv;
            invGui.current_gui_action = InventoryGUI.Inventory_Gui_Type.ChestInventory;
            
           
            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
            invGui.Show = true;
            if (OnCloseChest != null)
                invGui.OnQuitButton.AddListener(OnCloseChest);

            anim.Play("open");
            hasBeenOpen = true;
        }
        else
        {
            anim.Play("close");
            hasBeenOpen = false;
        }

        //anim.Play("idle");
    }
    
    private void CloseChest()
    {
        if (hasBeenOpen == true)
        {
            anim.Play("close");
            hasBeenOpen = false;
        }

        //anim.Play("idle");

        invGui.OnQuitButton.RemoveListener(OnCloseChest);
    }

}
