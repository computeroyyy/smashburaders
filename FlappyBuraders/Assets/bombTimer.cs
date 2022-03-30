using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombTimer : MonoBehaviour
{
    public float timex = 4;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(SFXS.TING);
        StartCoroutine(timeBomb());
    }

    IEnumerator timeBomb() {
        yield return new WaitForSeconds(timex);
        AudioManager.instance.PlaySFX(SFXS.EXPLOSION);
        GameObject boom;
        if (timex == 4)
            boom = Resources.Load("Effects/Boom") as GameObject;
        else 
            boom = Resources.Load("Effects/Boom2") as GameObject;
        boom = Instantiate(boom, this.transform.position, this.transform.rotation);
        Destroy(boom, 1.2f);
        Destroy(this.gameObject);
    }
    public void Explode() {
        StopCoroutine("timeBomb");
        AudioManager.instance.PlaySFX(SFXS.EXPLOSION);
        GameObject boom;
        if (timex == 4)
            boom = Resources.Load("Effects/Boom") as GameObject;
        else 
            boom = Resources.Load("Effects/Boom2") as GameObject;
        boom = Instantiate(boom, this.transform.position, this.transform.rotation);
        Destroy(boom, 1.2f);
        Destroy(this.gameObject);
    }
    void FixedUpdate()
    {
        this.transform.position += Vector3.right * Time.deltaTime * 2;
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 10) {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(this.GetComponent<Rigidbody2D>().velocity, 20);
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            Explode();
        }
    }
}
