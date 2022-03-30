using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidlatScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySFX(SFXS.GUNHIT);
        StartCoroutine(taposNa());
    }

    IEnumerator taposNa() {
        yield return new WaitForSecondsRealtime(0.2f);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D other) {
        // stunnedDuration += 0.3f;
        if (other.gameObject.tag == "Player") {
            AudioManager.instance.PlaySFX(SFXS.MEDIUMHIT);
            StartCoroutine(other.gameObject.GetComponent<YourBurader>().StunnedRoutine(0.75f));
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GameObject faya = Resources.Load("Particles/Spark") as GameObject;
            faya = Instantiate(faya, other.transform.position, Quaternion.identity, other.transform);
            Destroy(faya, 1.3f);
            other.gameObject.GetComponent<YourBurader>().HPCurrent -= 10;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
