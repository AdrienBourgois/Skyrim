using UnityEngine;

public class Enemy : ACharacter, IHitable
{
    public void OnHit(ACharacter entity)
    {
        transform.position += Vector3.up;
    }
}
