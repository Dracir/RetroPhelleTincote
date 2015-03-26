using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GloudIdle : State {
	
	Gloud Layer {
		get { return ((Gloud)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void TriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			Rigidbody2D playerRigidbody = collision.gameObject.GetComponentInParent<Rigidbody2D>();
			playerRigidbody.velocity *= Layer.damping;
			
			SwitchState<GloudDespawning>();
			return;
		}
	}
}
