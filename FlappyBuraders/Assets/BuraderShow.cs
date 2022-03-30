using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FlappyBuraders.GlobalStuff;

public class BuraderShow : MonoBehaviour
{
    public GameObject buraderX;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(load());
    }
    IEnumerator load() {
        buraderX.SetActive(true);
        GameObject change = Resources.Load("Buraders/" + ((Enums.BuraderName) Random.Range(0, 21)).ToString() + "X") as GameObject;
        buraderX.GetComponentInChildren<Image>().sprite = change.GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(6);
        buraderX.SetActive(false);
        StartCoroutine(load());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
