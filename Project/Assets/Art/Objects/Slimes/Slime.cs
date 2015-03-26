using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Slime : StateLayer {
	
	public Vector2 gravity;
	[Min] public float maxSpeed = 5;
	[Min] public float lifeTime = 30;
	[Min] public float scaleSpeed = 3;
	public AudioClip[] hitSound;
	
	bool _rigidbodyCached;
	Rigidbody2D _rigidbody;
	new public Rigidbody2D rigidbody { 
		get { 
			_rigidbody = _rigidbodyCached ? _rigidbody : GetComponent<Rigidbody2D>();
			_rigidbodyCached = true;
			return _rigidbody;
		}
	}
	
	AudioSource _source;
	public AudioSource source {
		get{
			if (_source == null){
				_source = gameObject.AddComponent<AudioSource>();
			}
			return _source;
		}
	}
	
	public void Move() {
		rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxSpeed);
		rigidbody.AddForce(gravity * Time.fixedDeltaTime, ForceMode2D.Impulse);
	}
	
	public bool CheckTrigger(Collider2D collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer("Specials")) {
			return false;
		}
		
		Rigidbody2D playerRigidbody = collision.GetComponentInParent<Rigidbody2D>();
		
		if (playerRigidbody && playerRigidbody.transform.tag == "Player") {
			playerRigidbody.SetVelocity(gravity);
		}
		
		return true;
	}
	public void PlaySound (AudioClip clip){
		source.PlayOneShot(clip, 1f);
	}
}
