using System.Security.Permissions;
using UnityEngine;
using System.Collections;
using Magicolo;

public class EndFlag : MonoBehaviour {

	float soundtimer;
	public AudioClip winSound;
	bool playing;
	
	void Update(){
		if(playing){
			soundtimer-= Time.deltaTime;
			if(soundtimer <=0){
				GameManager.instance.nextLevel();
			}
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D other) {
		if(playing) return;
		
		Rigidbody2D body = other.GetComponentInParent<Rigidbody2D>();
		if(body && body.transform.tag == "Player"){
			winSound = body.transform.root.GetComponent<PlayerSFX>().victoryFX;
			soundtimer = winSound.length;
			playing = true;
			gameObject.AddComponent<AudioSource>().PlayOneShot(winSound, 1f);
		}
	}
}
