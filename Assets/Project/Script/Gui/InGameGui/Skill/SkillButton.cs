using UnityEngine;

public class SkillButton : MonoBehaviour
{
    GameObject panel;

    void Start()
    {
        panel = transform.parent.FindChild("SkillPanel").gameObject;
        panel.SetActive(false);
    }

    public void OnClick()
    {
        panel.SetActive(true);
    }
}
