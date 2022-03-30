using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxx : MonoBehaviour
{
    private float length, startPos;
	GameObject cam;
	public float parallexEffect;

	void Start () {
		cam = Camera.main.gameObject;
		startPos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	void FixedUpdate () {
        
		float temp = (cam.transform.position.x * (1-parallexEffect));
		float dist = (cam.transform.position.x*parallexEffect);

		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

		if      (temp > startPos + length) startPos += length;
		else if (temp < startPos - length) startPos -= length;
	}
}
