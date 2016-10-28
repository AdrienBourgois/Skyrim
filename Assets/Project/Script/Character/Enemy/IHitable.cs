public interface IHitable
{
    void OnHit(ACharacter _character);
    void OnHit(ACharacter _character, float _spellDamages);
}
