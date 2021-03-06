using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class BGM {
	public string name;
	public AudioClip clip;
	[Range(0f, 1f)]
	public float volume = 1f;

	[HideInInspector]
	public AudioSource source;
	public bool loop = true;
}
