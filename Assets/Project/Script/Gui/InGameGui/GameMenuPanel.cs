using UnityEngine;

public class GameMenuPanel : MonoBehaviour {

    [Useless]
    public void Show()
    {
        gameObject.SetActive(true);
    }

    [Useless]
    public void Close()
    {
        gameObject.SetActive(false);
    }

    [Useless]
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
