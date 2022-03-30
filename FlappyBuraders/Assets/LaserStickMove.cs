using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStickMove : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform Target;
    public float rotatespeed = 99999;
    public float movespeed = 1234;
    Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector2 (
            Target.position.x - this.transform.position.x,
            Target.position.y - this.transform.position.y
        );
        dir = dir.normalized;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * movespeed * Time.deltaTime;

        Vector3 targetVector = Target.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector.normalized, transform.up).z;
        // float rotatingIndex = Vector3.Cross(targetVector.normalized, dir).z;

        rb.angularVelocity = -1 * rotatingIndex * rotatespeed * Time.deltaTime; //rotatespeed

    }
    public void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Player") {
            AudioManager.instance.PlaySFX(SFXS.SLASH);
            StartCoroutine(other.gameObject.GetComponent<YourBurader>().StunnedRoutine(0.5f));
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            other.gameObject.GetComponent<YourBurader>().otherBurader.SpecialKillTimer = 0.5f;
            other.gameObject.GetComponent<YourBurader>().HPCurrent -= 5;
            other.gameObject.GetComponent<YourBurader>().otherBurader.ComboAddShow();
        }
    }
}
