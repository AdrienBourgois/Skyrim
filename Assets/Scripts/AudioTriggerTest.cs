using UnityEngine;

public class AudioTriggerTest : MonoBehaviour {
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Spine")
            AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Fight);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Spine")
            AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Game);
    }
}
