using UnityEngine;
using System.Collections;

public class InventoryButton : MonoBehaviour
{
    GameObject panel;

    void Start()
    {
        panel = transform.parent.FindChild("InventoryPanel").gameObject;
        panel.SetActive(false);
    }
	
    public void OnClick()
    {
        panel.SetActive(true);
    }
}
