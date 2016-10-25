using UnityEngine;
using System.Collections;

public abstract class ASpell : MonoBehaviour {

    SpellProperty magicProperties;

    ACharacterController selfController;

	void Start ()
    {
	    
	}

    public void SetController(ACharacterController controller)
    {
        selfController = controller;
    }

    public abstract void Activate();

}
