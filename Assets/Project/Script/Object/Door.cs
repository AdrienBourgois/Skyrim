using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animation))]
public class Door : MonoBehaviour, IUsableObject
{

    private Animation anim = null;
    private bool hasBeenOpen = false;


    public void OnUse(ACharacter character)
    {
        print("OnUse");

        if (hasBeenOpen == false)
        {
            print("test");
            anim.Play("CloseDoor");
            hasBeenOpen = true;
        }
        else if (hasBeenOpen == true)
        {
            anim.Play("CloseDoor");
        }
    }

    void Start () {

        anim = GetComponent<Animation>();
    }
	
	void Update () {
	
	}
}
