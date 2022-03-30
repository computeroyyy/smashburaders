using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShogyoEffect : MonoBehaviour
{
    void Start() {
        Destroy(this.gameObject, 2f);
    }
    // public void OnCollisionEnter2D(Collision2D other) {

    //     if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<YourBurader>().buraderType != YourBurader.BuraderType.OYYY) {
    //         StartCoroutine(other.gameObject.GetComponent<YourBurader>().otherBurader.Stunned(0.5f));
        
    //         // force is how forcefully we will push the player away from the enemy.
    //         float force = 20;
    //         // Calculate Angle Between the collision point and the player
    //         Vector3 dir = other.contacts[0].point - new Vector2(this.transform.position.x, this.transform.position.y);
    //         // We then get the opposite (-Vector3) and normalize it
    //         dir = dir.normalized;
    //         // And finally we add force in the direction of dir and multiply it by force. 
    //         // This will push back the player
    //         if (other.gameObject.tag != "Wall")
    //             other.gameObject.GetComponent<Rigidbody2D>().velocity = (dir * force);
    //     }
    // }
}
