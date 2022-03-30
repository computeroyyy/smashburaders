using System.Collections;
using UnityEngine;

public class MassimoBuilding : MonoBehaviour
{
    public void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag != "Wall") {
            if (speed > 0)
                other.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.right * speed * 2);
        }
    }
    public void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (risingUp) {
                other.gameObject.GetComponent<YourBurader>().HPCurrent -= 2;
                StartCoroutine(other.gameObject.GetComponent<YourBurader>().StunnedRoutine(1f));
                other.gameObject.GetComponent<YourBurader>().otherBurader.ComboAddShow();
            }
            if (fallingDown){
                other.gameObject.GetComponent<YourBurader>().HPCurrent -= 1;
                other.gameObject.GetComponent<YourBurader>().otherBurader.ComboAddShow();
            }
        }
    }

    void Awake() {
        
    }
    void Start() {
        StartCoroutine(move());

    }
    void FixedUpdate()
    {
        this.transform.parent.position += Vector3.right * Time.deltaTime * speed;
        if (speed > 0) {
            speed -= Time.deltaTime;
        }
        if (rotate != 0) {
            this.transform.parent.eulerAngles = Vector3.forward * rotate;
            rotate -= Time.deltaTime * 50;
        }
        
    }
    float speed = 0;
    float rotate = 0;
    bool fallingDown = false;
    bool risingUp = false;

    IEnumerator move() {
        risingUp = true;
        yield return new WaitForSeconds(0.25f);
        risingUp = false;
        yield return new WaitForSeconds(0.75f);
        speed = 2;
        yield return new WaitForSeconds(2.5f);
        rotate = -0.1f;
        fallingDown = true;
    }

    
}
