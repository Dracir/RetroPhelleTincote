using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SlimeDespawning : State {
	
    Slime Layer {
    	get { return ((Slime)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.rigidbody.SetVelocity(Layer.rigidbody.velocity / 10);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		transform.localScale = Vector3.zero;
		Destroy(gameObject);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		transform.ScaleLocalTowards(Vector3.zero, Layer.scaleSpeed);
		
		if (transform.localScale.magnitude < 0.1F) {
			SwitchState<SlimeIdle>();
			return;
		}
	}
}
