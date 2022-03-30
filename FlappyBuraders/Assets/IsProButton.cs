using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBuraders.GlobalStuff;
using UnityEngine.UI;

public class IsProButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LocalData.hasGonePRO) {
            GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
