using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TardigoodStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(uhh());
        StartCoroutine(ahh());
    }
    bool uh;
    IEnumerator uhh() {
        yield return new WaitForSeconds(5);
        uh = !uh;
        StartCoroutine(uhh());
    }
    IEnumerator ahh() {
        yield return new WaitForSeconds(60);
        GameObject blad = Resources.Load("Effects/BloodSplatSmol") as GameObject;
        blad = Instantiate(blad, this.gameObject.transform.position, this.transform.rotation);
        Destroy(blad, 7.5f);
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (uh) {
            // this.transform.position += Vector3.right * Time.deltaTime * 0.5f;
            this.GetComponent<Rigidbody2D>().gravityScale = -0.5f;
        }
        else {
            //this.transform.position += Vector3.right * Time.deltaTime * -0.5f;
            this.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        }
    }
    void FixedUpdate() {
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 5) {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(this.GetComponent<Rigidbody2D>().velocity, 6);
        }
    }
}
