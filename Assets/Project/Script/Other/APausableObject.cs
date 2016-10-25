using UnityEngine;

public class APausableObject : MonoBehaviour {

    protected bool paused = false;

	virtual protected void PutPause()
    {
        paused = !paused;
    }
}
