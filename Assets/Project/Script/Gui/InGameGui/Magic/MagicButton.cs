using UnityEngine;
using System.Collections;

public class MagicButton : MonoBehaviour
{
    GameObject panel;

	void Start ()
    {
        panel = transform.parent.FindChild("MagicPanel").gameObject;
        panel.SetActive(false);
    }

    public void OnClick()
    {
        panel.SetActive(true);
    }
}
