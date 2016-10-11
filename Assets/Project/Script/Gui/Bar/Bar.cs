using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {

    protected Player player;

    protected RectTransform bar;
    protected Text point;

    void Start ()
    {
        GameObject player_gao = GameObject.FindGameObjectWithTag("Player");
        if (player_gao)
            player = player_gao.GetComponent<Player>();

        bar = transform.FindChild("Bar").GetComponent<RectTransform>();
        point = transform.FindChild("Point").GetComponent<Text>();
    }
}
