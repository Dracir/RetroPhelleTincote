using UnityEngine;
using System.Collections;

public class RandomSoundPlayer : MonoBehaviour {

	public AudioClip[] sounds;
	
	AudioSource source;
	void Start () {
		source = gameObject.AddComponent<AudioSource>();
	}
	
	void PlaySound () {
		PlaySound(1f);
	}
	
	void PlaySound (float soundScale) {
		int i = Random.Range (0, sounds.Length);
		source.PlayOneShot(sounds[i], soundScale);
	}
	
}
