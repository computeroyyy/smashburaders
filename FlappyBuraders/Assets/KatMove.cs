using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatMove : MonoBehaviour
{
    private float RotateSpeed = 12;
    private float Radius = 1f;
 
    private Vector2 center;
    private float _angle;
    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }
 
    private void Update()
    {  
        // center = this.transform.parent.position;
        // // _angle += RotateSpeed * Time.deltaTime;

        // // var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        // this.transform.position = center;// + offset;
    }

    public void OnTriggerEnter2D (Collider2D other) {
        // float force = 10;
        // Vector3 dir = other.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
        // dir = dir.normalized;
        if (other.gameObject.tag == "Player") {
            AudioManager.instance.PlaySFX(SFXS.EXPLOSION);
            StartCoroutine(other.gameObject.GetComponent<YourBurader>().StunnedRoutine(0.75f));
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GameObject faya = Resources.Load("Particles/Fire") as GameObject;
            faya = Instantiate(faya, other.transform.position, Quaternion.identity, other.transform);
            Destroy(faya, 1.3f);
            other.gameObject.GetComponent<YourBurader>().otherBurader.SpecialKillTimer = 1f;
            other.gameObject.GetComponent<YourBurader>().HPCurrent -= 4;
        }
        // if (other.gameObject.tag != "Wall" && other.gameObject.tag != "Player" && other.gameObject.tag != "Invincible") {
        //     other.gameObject.GetComponent<Rigidbody2D>().velocity = (dir * force);
        // }
    }
}
