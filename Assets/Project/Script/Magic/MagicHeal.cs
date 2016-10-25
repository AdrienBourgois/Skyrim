using UnityEngine;

public class MagicHeal : AMagic
{
    public override void Activate()
    {
        // TODO: heal selfCharacter
        Debug.Log("HEAL");
        Destroy(gameObject);
    }
}
