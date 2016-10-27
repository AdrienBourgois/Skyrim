using UnityEngine;

public class APausableObject : MonoBehaviour {

    protected bool paused;

	virtual protected void PutPause()
    {
        paused = !paused;
    }
}
