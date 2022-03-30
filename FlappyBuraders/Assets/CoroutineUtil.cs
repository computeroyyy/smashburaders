using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtil : MonoBehaviour
{
    // Start is called before the first frame update
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
