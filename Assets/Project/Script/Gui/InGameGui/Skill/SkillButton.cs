using UnityEngine;

public class SkillButton : MonoBehaviour
{
    private GameObject panel;

    private void Start()
    {
        panel = transform.parent.FindChild("SkillPanel").gameObject;
        panel.SetActive(false);
    }

    public void OnClick()
    {
        panel.SetActive(true);
    }
}
