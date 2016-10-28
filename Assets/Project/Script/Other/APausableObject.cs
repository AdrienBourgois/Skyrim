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
        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
