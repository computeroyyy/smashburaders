using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class VOX {
	public string name;
	public AudioClip clip;
	[Range(0f, 1f)]

	[HideInInspector]
	public AudioSource source;
	
	
}
