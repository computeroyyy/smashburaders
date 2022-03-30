using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulScript : MonoBehaviour
{
    Rigidbody2D rigidbody = new Rigidbody2D();
    GameObject Flappy1;
    GameObject Flappy2;
    Camera cameraMain;
    public bool moveRight;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(0, 1f)) * Random.Range(700f, 1200f));
        cameraMain = Camera.main;
        StartCoroutine(move());
        Flappy1 = GameObject.Find("Flappy1");
        Flappy2 = GameObject.Find("Flappy2");
    }

    IEnumerator move() {
        yield return new WaitForSeconds(1);
        if (!moveRight) {
            Vector2 dir = new Vector2 (
                cameraMain.transform.position.x - 1.6f - this.transform.position.x,
                cameraMain.transform.position.y - 3.4f - this.transform.position.y
            );
            dir = dir.normalized;
            this.GetComponent<Rigidbody2D>().velocity = dir.normalized * 40;
            Flappy1.GetComponent<YourBurader>().SpecialCurrent += 2;
        }
        else {
            Vector2 dir = new Vector2 (
                cameraMain.transform.position.x + 1.8f - this.transform.position.x,
                cameraMain.transform.position.y - 3.4f - this.transform.position.y
            );
            dir = dir.normalized;
            this.GetComponent<Rigidbody2D>().velocity = dir.normalized * 40;
            Flappy2.GetComponent<YourBurader>().SpecialCurrent += 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < cameraMain.transform.position.y - 3.4f) {
            AudioManager.instance.PlaySFX(SFXS.POP);
            Destroy(this.gameObject);
        }
    }
}
