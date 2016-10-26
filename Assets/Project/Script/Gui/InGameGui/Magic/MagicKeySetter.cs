using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagicKeySetter : MonoBehaviour {

	
	void Awake ()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Button>())
            {
                int keyNumber = int.Parse(child.GetChild(0).GetComponent<Text>().text);
                child.GetComponent<Button>().onClick.AddListener(delegate { SetMagic(keyNumber); });
            }
        }
        Close();
    }

    public void Show()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void SetMagic(int key_number)
    {
        SpellProperty selectedMagic = transform.parent.GetComponent<MagicPanel>().DisplayedMagic;
        if (selectedMagic == null)
            return;

        transform.GetChild(key_number).FindChild("name").GetComponent<Text>().text = selectedMagic.MagicName.ToString();
        MagicManager.Instance.MagicKeySelected[key_number] = selectedMagic;

    }
}
