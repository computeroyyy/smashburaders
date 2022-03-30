using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMove : MonoBehaviour
{
    
    void Start() {
        Destroy(this.gameObject, 0.8f);
        StartCoroutine(Invincibility(90));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += Vector3.right * Time.deltaTime * 2;
    }
    IEnumerator Invincibility(int count) {
        for (int x = 0; x < count; x++) {
            this.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.02f);
            this.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
