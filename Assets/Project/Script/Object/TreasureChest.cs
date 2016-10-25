using UnityEngine;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{
    private Animation anim = null;
    private bool hasBeenOpen = false;


    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void OnUse(ACharacter character)
    {
        if (hasBeenOpen == false)
        {
            anim.Play("open");
            hasBeenOpen = true;
        }
        else
        {
            anim.Play("close");
            hasBeenOpen = false;
        }
    }
}
