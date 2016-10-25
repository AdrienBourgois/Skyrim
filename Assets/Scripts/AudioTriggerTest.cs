using UnityEngine;

public class AudioTriggerTest : MonoBehaviour {
    private void OnTriggerEnter()
    {
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Fight);
    }

    private void OnTriggerExit()
    {
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Game);
    }
}
