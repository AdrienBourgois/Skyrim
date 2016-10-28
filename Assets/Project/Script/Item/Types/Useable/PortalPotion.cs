public class PortalPotion : Useable, IUseableItem, IInstanciableItem
{

    public void Use()
    {
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.LoadGame);
        GameManager.Instance.PauseInit();
    }

    public void Instantiate()
    {
        Price = 200;
        Weight = 1;
    }

    public void SetRandName()
    {
        NameObject = "Portal Potion";
    }

    public void SetRandDescription()
    {
        Description = "Return to spawn";
    }
}
