public class InvisibleSpell : ASpell
{
    public override void Activate()
    {
        Destroy(gameObject);
    }
}
