using UnityEngine;

public class MagicButton : MonoBehaviour
{
    private GameObject panel;

    private void Start ()
    {
        panel = transform.parent.FindChild("MagicPanel").gameObject;
        panel.SetActive(false);
    }

    public void OnClick()
    {
        panel.SetActive(true);
    }
}
