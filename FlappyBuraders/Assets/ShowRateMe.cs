using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBuraders.GlobalStuff;

public class ShowRateMe : MonoBehaviour
{
    public GameObject RateMePanel;
    // Start is called before the first frame update
    void Start()
    {
        if(LocalData.toShowRateMe && !LocalData.alreadyShownRateMe) {
            RateMePanel.SetActive(true);
            LocalData.toShowRateMe = false;
            LocalData.alreadyShownRateMe = true;
        }
        else {
            RateMePanel.SetActive(false);
        }
    }
}
