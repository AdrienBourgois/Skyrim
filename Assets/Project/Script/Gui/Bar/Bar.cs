using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    protected Player player;

    protected RectTransform bar;
    protected Text point;

    private void Start ()
    {
        player = FindObjectOfType<Player>();

        bar = transform.FindChild("Bar").GetComponent<RectTransform>();
        point = transform.FindChild("Point").GetComponent<Text>();
    }
}
