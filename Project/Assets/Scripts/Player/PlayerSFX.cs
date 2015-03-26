using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSFX : MonoBehaviour {
	
	public AudioClip[] walkFX;
	public AudioClip[] jumpFX;
	public AudioClip deathFX;
	public AudioClip victoryFX;
	
	ModelJump jumper;
	AudioSource walkSource;
	AudioSource jumpSource;
	
	int walkIndex = 0;
	List<AudioSource> sourceChildren = new List<AudioSource>();
	
	void Start () {
		jumper = GetComponent<ModelJump>();
		walkSource = MakeSoundChild();
		jumpSource = MakeSoundChild();
	}
	
	void FootstepSound(){
		if (!jumper.Grounded){
			return;
		}
		
		AudioClip clip = walkFX[walkIndex];
		walkSource.PlayOneShot(clip, 1f);
		
		walkIndex ++;
		if (walkIndex == walkFX.Length){
			walkIndex = 0;
		}
	}
	
	void JumpSound(){
		AudioClip clip = jumpFX[Random.Range(0, jumpFX.Length)];
		jumpSource.PlayOneShot(clip, 1f);
	}
	
	void WinSound () {
		AudioSource source = GetSoundChild();
		source.PlayOneShot(victoryFX, 1f);
		
	}
	
	void DeathSound () {
		AudioSource source = GetSoundChild();
		source.PlayOneShot(deathFX, 1f);
		
	}
	
	void Respawn () {
		DeathSound();
	}
	
	AudioSource GetSoundChild(){
		AudioSource prospect = null;
		foreach (AudioSource source in sourceChildren) {
			if (!source.isPlaying){
				prospect = source;
			}
		}
		if (prospect == null){
			prospect = new GameObject("Source " + sourceChildren.Count).AddComponent<AudioSource>();
			prospect.transform.parent = transform;
			prospect.transform.localPosition = Vector3.zero;
		}
		return prospect;
	}
	
	AudioSource MakeSoundChild () {
		AudioSource prospect = new GameObject("Source " + sourceChildren.Count).AddComponent<AudioSource>();
		sourceChildren.Add(prospect);
		prospect.transform.parent = transform;
		prospect.transform.localPosition = Vector3.zero;
		return prospect;
	}
	
	
}
