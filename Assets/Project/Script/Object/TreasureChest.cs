using UnityEngine;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{
    private Animation anim;
    private bool hasBeenOpen;
    private InventoryPanelGui invPanelGui;
    private Inventory inv = new Inventory();
    private UnityEngine.Events.UnityAction onCloseChest;


    private void Awake()
    {
        anim = GetComponent<Animation>();

        invPanelGui = InventoryPanelGui.Instance;
    }

    private void Start()
    {
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.FlagsGeneration.AllType, 10);

        onCloseChest = CloseChest;
    }

    public void OnUse(ACharacter _character)
    {
        if (hasBeenOpen == false)
        {
            invPanelGui.Inventory = inv;
            invPanelGui.currentGuiAction = InventoryPanelGui.InventoryGuiType.ChestInventory;
            
           
            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
            invPanelGui.Show = true;
            if (onCloseChest != null)
                invPanelGui.onQuitButton.AddListener(onCloseChest);

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


        invPanelGui.onQuitButton.RemoveListener(onCloseChest);
    }

}
