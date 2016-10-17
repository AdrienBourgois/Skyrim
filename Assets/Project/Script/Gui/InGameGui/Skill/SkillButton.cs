using UnityEngine;
using System.Collections;

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
        panel.transform.FindChild("AttribPanel").GetComponent<AttribPanel>().Init();
        panel.transform.FindChild("CharacPanel").GetComponent<CharacPanel>().Init();
    }
}
