using UnityEngine;

public class ReturnButton : MonoBehaviour
{
	
    public void OnClick()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
