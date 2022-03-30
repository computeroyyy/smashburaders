using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBuraders.GlobalStuff;

public class SiponEffect : MonoBehaviour
{
    void Start () {
        Destroy (this.gameObject, 10f);
    }
    void FixedUpdate() {
        // this.transform.position += Vector3.right * Time.deltaTime * 2;
        // this.GetComponent<Rigidbody2D>().drag -= Time.deltaTime / 2;
    }
    // void OnTriggerEnter2D(Collider2D other) {
    //     ////Debug.Log("sipon");
    //     if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<YourBurader>().buraderType != Enums.BuraderType.PAU) {
    //         other.gameObject.GetComponent<Rigidbody2D>().drag = 70;
    //     }
    // }
    // void OnTriggerExit2D(Collider2D other) {
    //     if (other.gameObject.tag == "Player"){
    //         other.gameObject.GetComponent<Rigidbody2D>().drag = 0;
    //     }
    // }
}
