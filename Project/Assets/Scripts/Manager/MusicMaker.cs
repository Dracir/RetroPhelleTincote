using UnityEngine;
using System.Collections;

public class MusicMaker : MonoBehaviour{
	
	public AudioClip[] musicFiles;
	int index;
	AudioSource source;
	// Use this for initialization
	void Start () {
		ChooseNPlay();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying){
			ChooseNPlay();
		}
	}
	
	void ChooseNPlay () {
		index = Random.Range(0, musicFiles.Length);
		source = GetComponent<AudioSource>();
		if (source == null){
			source = gameObject.AddComponent<AudioSource>();
			source.clip = musicFiles[index];
			source.Play();
		}
	}
}
