using UnityEngine;
using System.Collections;

public class AudioTriggerTest : MonoBehaviour {

	void OnTriggerEnter()
    {
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Fight);
    }

    void OnTriggerExit()
    {
        AudioManager.Instance.PlayMusic(AudioManager.EMusic_Type.Game);
    }
}
