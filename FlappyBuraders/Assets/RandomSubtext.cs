using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSubtext : MonoBehaviour
{
    
    void Start()
    {
        this.GetComponent<Text>().text = PromptMessage.GetRandomSubTitle().MainMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
