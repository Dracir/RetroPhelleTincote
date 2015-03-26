using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SlimeSpawning : State {
	
	[Disable] public Vector3 scale;
	
	Slime Layer {
		get { return ((Slime)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		scale = transform.localScale;
		transform.localScale = Vector3.zero;
	}
	
	public override void OnExit() {
		base.OnExit();
		
		transform.localScale = scale;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		transform.ScaleLocalTowards(scale, Layer.scaleSpeed);
		
		if (Vector3.Distance(transform.localScale, scale) <= 0.1F) {
			SwitchState<SlimeMoving>();
			return;
		}
	}
		
	public override void OnFixedUpdate() {
		base.OnFixedUpdate();
		
		Layer.Move();
	}
	
	public override void TriggerEnter2D(Collider2D collision) {
		base.TriggerEnter2D(collision);
		
		if (Layer.CheckTrigger(collision)) {
			SwitchState<SlimeDespawning>();
			return;
		}
	}
}
