using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPoop : MonoBehaviour
{
    public GameObject myDoge;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), myDoge.gameObject.GetComponent<CircleCollider2D>());        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * 2;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(this.GetComponent<Rigidbody2D>().velocity, 16);
    }
}
