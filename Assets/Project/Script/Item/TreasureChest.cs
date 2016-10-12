using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Animation))]
public class TreasureChest : MonoBehaviour, IUsableObject
{

    private Animation anim = null;
    private bool hasBeenOpen = false;


    public void OnUse(ACharacter character)
    {
        if (hasBeenOpen == false)
        {
            anim.Play("open");
            hasBeenOpen = true;
        }

    }


    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    void Start()
    {
	    
    }	
}
