using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuraderMove : MonoBehaviour
{
    float force = 5.9f;
    YourBurader thisBurader;
    void Start() {
        thisBurader = this.GetComponent<YourBurader>();
    }
    public void MoveLeftBtn() {
        MoveLeft();
    }
    private void MoveLeft() {
        thisBurader.isMovingLeft += Time.deltaTime;
        JumpTowards(thisBurader, 1);
    }
    public void MoveRightBtn() {
        MoveRight();
    }
    private void MoveRight() {
        thisBurader.isMovingRight += Time.deltaTime;
        JumpTowards(thisBurader, -1);
    }
    public void ThisBreak() {
        Break(thisBurader);
    }
    public void ThisRelease() {
        MoveRelease(thisBurader);
    }
    
    private void JumpTowards(YourBurader theBurader, int dir) {
        
        if (theBurader.isMovingLeft > 0) {
            if (theBurader.isMovingLeft < theBurader.isMovingRight) {
                dir = 1;
            }
        } 
        if (theBurader.isMovingRight > 0) {
            if (theBurader.isMovingRight < theBurader.isMovingLeft) {
                dir = -1;
            }
        }
        if (theBurader.stunnedDuration <= 0) {
            if (theBurader.gameObject != null) {
                float angle = -40;
                int ericBug = 1;

                if (theBurader.EricSkill) {
                    ericBug = Random.Range(-7, 7);
                }

                theBurader.transform.rotation = Quaternion.Euler(0,0,(angle + (theBurader.acceleration * 50)) * dir * ericBug);
                theBurader.GetComponent<Rigidbody2D>().velocity = theBurader.transform.up * force * theBurader.acceleration;

                if (theBurader.BryanSkill) {
                    theBurader.otherBurader.transform.rotation = Quaternion.Euler(0,0,angle * dir);
                    theBurader.otherBurader.GetComponent<Rigidbody2D>().velocity = theBurader.transform.up * force * theBurader.acceleration;
                }
                
                theBurader.acceleration += Time.deltaTime * (3 + (theBurader.acceleration / 2));
            }
        }
    }
    public void Break(YourBurader theBurader) {
        if (theBurader.stunnedDuration <= 0) {
            theBurader.acceleration = 1;
            if (theBurader.gameObject != null) {
                if (theBurader.GetComponent<Rigidbody2D>().velocity.magnitude > 12) {  
                    GameObject flash = Resources.Load("Particles/FLASH") as GameObject;
                    flash = Instantiate(flash, theBurader.transform.position, theBurader.transform.rotation, theBurader.transform);
                    AudioManager.instance.PlaySFX(SFXS.SPLASH);
                    theBurader.GetComponent<Rigidbody2D>().drag = 35;
                    theBurader.transform.rotation = Quaternion.Euler(0,0,0);
                    theBurader.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }
        }
    }
    public void MoveRelease(YourBurader theBurader) {
        theBurader.isMovingLeft = 0;
        theBurader.isMovingRight = 0;
    }

}
