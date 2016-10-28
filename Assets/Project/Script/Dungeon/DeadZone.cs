using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("OntriggerEnter");
            GameManager.Instance.ChangeGameStateTo(GameManager.GameState.Death);
            Destroy(collider.gameObject);
            Destroy(FindObjectOfType<Compass>());
        }
    }
}
