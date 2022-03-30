using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    public float duration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, duration);
    }

}
