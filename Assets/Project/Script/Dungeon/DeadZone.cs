using UnityEngine;

public class DeadZone : MonoBehaviour {

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.gameObject.tag == "Player")
        {
            Debug.Log("OntriggerEnter");
            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Death);
            Destroy(_collider.gameObject);
            Destroy(FindObjectOfType<Compass>());
        }
    }
}
