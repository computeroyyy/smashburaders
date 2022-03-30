using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            AudioManager.instance.PlaySFX(SFXS.SMALLHIT);
            // GameObject nano = Resources.Load("Particles/SMOKE") as GameObject;
            // nano = Instantiate(nano, this.transform.position, Camera.main.transform.rotation, Camera.main.transform);
            
            Destroy(this.gameObject);
        }
    }
}
