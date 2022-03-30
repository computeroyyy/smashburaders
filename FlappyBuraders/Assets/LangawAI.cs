using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBuraders.GlobalStuff;

public class LangawAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameController.gameMode == Enums.GameMode.HOLD_HANDS) {
            this.gameObject.tag = "Langaw";
        }
        else 
            this.gameObject.tag = "PlayerClone";
        StartCoroutine(changeBot());
    }
    public void TwoLeft() {
        if (this.gameObject != null) {
            this.transform.rotation = Quaternion.Euler(0,0,20);
            this.GetComponent<SpriteRenderer>().flipX = false;
            this.GetComponent<Rigidbody2D>().velocity = this.transform.up * 5;
        }
    }
    public void TwoRight() {
        if (this.gameObject != null) {
            this.transform.rotation = Quaternion.Euler(0,0,-20);
            this.GetComponent<SpriteRenderer>().flipX = true;
            this.GetComponent<Rigidbody2D>().velocity = this.transform.up * 5;
        }
    }
    float botSize;
    IEnumerator changeBot() {
        yield return new WaitForSeconds(2f);
        botSize = Random.Range(1f, 8f);
        StartCoroutine(changeBot());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > 5) {
            this.GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(this.GetComponent<Rigidbody2D>().velocity, 10);
        }
        int layerMask = (int)LayerMask.NameToLayer("Killer"); //look only for base
        int layerMaskTop = (int)LayerMask.NameToLayer("Wall"); //look only for base

        Vector3 top = new Vector2(this.transform.position.x, this.transform.position.y + 0.5f);
        Vector3 mid = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector3 bot = new Vector2(this.transform.position.x, this.transform.position.y - 0.5f);
        Vector3 botx = new Vector2(this.transform.position.x, this.transform.position.y - 1.5f);

        RaycastHit2D hitRightTop = Physics2D.Raycast(top, Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(top, Vector2.right * 4);
        RaycastHit2D hitRightBotx = Physics2D.Raycast(botx, Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(botx, Vector2.right * 4);
        RaycastHit2D hitRightMid = Physics2D.Raycast(mid, Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(mid, Vector2.right * 4);
        RaycastHit2D hitRightBot = Physics2D.Raycast(bot, Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(bot, Vector2.right * 4);

        RaycastHit2D hitLeftTop = Physics2D.Raycast(top, -Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(top, -Vector2.right * 4);
        RaycastHit2D hitLeftMid = Physics2D.Raycast(mid, -Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(mid, -Vector2.right * 4);
        RaycastHit2D hitLeftBotX = Physics2D.Raycast(botx, -Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(botx, -Vector2.right * 4);
        RaycastHit2D hitLeftBot = Physics2D.Raycast(bot, -Vector2.right, 4, 1 << layerMask);
        Debug.DrawRay(bot, -Vector2.right * 4);
        
        RaycastHit2D hitTop = Physics2D.Raycast(this.transform.position, Vector2.up, 3.5f, 1 << layerMaskTop);
        Debug.DrawRay(this.transform.position, Vector2.up * 1.5f);

        RaycastHit2D hitTBot = Physics2D.Raycast(this.transform.position, -Vector2.up, botSize, 1 << layerMaskTop);
        Debug.DrawRay(this.transform.position, -Vector2.up * botSize);

        // If it hits top wall...
        if (hitTop.collider != null)
            return;

        if (canMove) {
            //found 
            if (hitRightTop.collider != null || hitRightBot.collider != null || hitRightBotx.collider != null || hitRightMid.collider != null) {
                TwoLeft();
            }
            if (hitLeftTop.collider != null || hitLeftBot.collider != null || hitLeftBotX.collider != null || hitLeftMid.collider != null) {
                TwoRight();
            }
            if (hitTBot.collider != null) {
                if (Random.Range(0, 2) == 0)
                    TwoLeft();
                else
                    TwoRight();
            }
        }
    }
    void Update() {
        if (stunnedDuration > 0) {
            canMove = false;
            stunnedDuration -= Time.deltaTime;
        }
        else {
            canMove = true;
        }
    }
    bool canMove = true;
    float stunnedDuration;
    public void OnCollisionEnter2D(Collision2D other) {
        // stunnedDuration += 0.3f;
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 15) {
                GameController.LangawKills++;

                AudioManager.instance.PlaySFX(SFXS.SLASH);
                Destroy(this.gameObject);
                GameObject blad = Resources.Load("Effects/BloodSplatSmol") as GameObject;
                blad = Instantiate(blad, this.gameObject.transform.position, this.transform.rotation);
                other.gameObject.GetComponent<YourBurader>().SpecialCurrent += 20;
                
                Destroy(blad, 7.5f);
            }
        }
    }
}
