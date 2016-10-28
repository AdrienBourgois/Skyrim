public class SelfSpell : ASpell
{
    public override void Activate()
    {
        selfController.Character.CharacterStats.UnitCharacteristics.Health += spellProperty.Power;
        Destroy(gameObject);
    }
}
