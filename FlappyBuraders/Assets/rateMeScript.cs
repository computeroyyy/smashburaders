using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rateMeScript : MonoBehaviour
{
    Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(OnClickRateMe);
    }

    private void OnClickRateMe() {
        Application.OpenURL("market://details?id=" + Application.identifier);
        this.transform.parent.gameObject.SetActive(false);
    }
}
