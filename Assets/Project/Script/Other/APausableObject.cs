using UnityEngine;

public class APausableObject : MonoBehaviour
{
    protected bool paused;

    protected virtual void Awake()
    {
        GameManager.OnPause += PutPause;
    }

    protected virtual void OnDestroy()
    {
        GameManager.OnPause -= PutPause;
    }

    protected virtual void PutPause()
    {
        paused = !paused;
    }
}
