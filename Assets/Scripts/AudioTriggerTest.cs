using UnityEngine;

public class AudioTriggerTest : MonoBehaviour {
    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.name == "Spine")
            AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Fight);
    }

    private void OnTriggerExit(Collider _collider)
    {
        if (_collider.gameObject.name == "Spine")
            AudioManager.Instance.PlayMusic(AudioManager.EMusicType.Game);
    }
}
