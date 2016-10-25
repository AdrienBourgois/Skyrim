using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{

    private Animation anim = null;
    private bool hasBeenOpen = false;


    public void OnUse(ACharacter character)
    {
        if (hasBeenOpen == false)
        {
            
            anim.Play("OpenDoor");
            hasBeenOpen = true;
        }
        else if (hasBeenOpen)
        {
            anim.Play("CloseDoor");
            hasBeenOpen = false;
        }
    }

    void Start () {

        anim = GetComponent<Animation>();
    }
	
	void Update () {
	
	}
}
