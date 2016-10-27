using UnityEngine;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{
    private Animation anim;
    private bool hasBeenOpen;
    InventoryGUI invGui;
    Inventory inv = new Inventory();
    UnityEngine.Events.UnityAction OnCloseChest;


    private void Awake()
    {
        anim = GetComponent<Animation>();

        invGui = InventoryGUI.Instance;
    }

    void Start()
    {
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.FlagsGeneration.AllType, 10);

        OnCloseChest = CloseChest;
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

    }
    
    private void CloseChest()
    {
        if (hasBeenOpen)
        {
            anim.Play("close");
            hasBeenOpen = false;
        }


        invGui.OnQuitButton.RemoveListener(OnCloseChest);
    }

}
