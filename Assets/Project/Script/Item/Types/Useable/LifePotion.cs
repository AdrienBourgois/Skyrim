public class LifePotion : Useable, IUseableItem, IInstanciableItem {

    public void Use()
    {
        Characteristics characteristics = LevelManager.Instance.Player.CharacterStats.UnitCharacteristics;
        characteristics.Health = characteristics.MaxHealth;
    }

    public void Instantiate()
    {
        Price = 300;
        Weight = 1;
    }

    public void SetRandName()
    {
        NameObject = "Life Potion";
    }

    public void SetRandDescription()
    {
        Description = "Simple life potion";
    }
}
