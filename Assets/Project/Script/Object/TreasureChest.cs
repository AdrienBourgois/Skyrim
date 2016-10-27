using UnityEngine;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{
    private Animation anim;
    private bool hasBeenOpen;
    private InventoryGui invGui;
    private Inventory inv = new Inventory();
    private UnityEngine.Events.UnityAction OnCloseChest;


    private void Awake()
    {
        anim = GetComponent<Animation>();

        invGui = InventoryGui.Instance;
    }

    private void Start()
    {
        inv.List = ItemManager.Instance.GenerateInventory(ItemManager.FlagsGeneration.AllType, 10);

        OnCloseChest = CloseChest;
    }

    public void OnUse(ACharacter character)
    {
        if (hasBeenOpen == false)
        {
            invGui.Inventory = inv;
            invGui.currentGuiAction = InventoryGui.InventoryGuiType.ChestInventory;
            
           
            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Pause);
            invGui.Show = true;
            if (OnCloseChest != null)
                invGui.onQuitButton.AddListener(OnCloseChest);

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


        invGui.onQuitButton.RemoveListener(OnCloseChest);
    }

}
