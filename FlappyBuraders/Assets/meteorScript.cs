using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * 260 * Time.deltaTime);
    }
    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(other.gameObject.GetComponent<YourBurader>().StunnedRoutine(0.2f));
            
            GameObject particle_hit = Resources.Load("Particles/hitheavy") as GameObject;
            particle_hit = Instantiate(particle_hit, this.transform.position, this.transform.rotation);
            AudioManager.instance.PlaySFX(SFXS.MEDIUMHIT);
        }
    }
}
