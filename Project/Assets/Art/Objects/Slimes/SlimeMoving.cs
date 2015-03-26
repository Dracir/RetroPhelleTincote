using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SlimeMoving : State {
	
	[Disable] public float counter;
	
	Slime Layer {
		get { return ((Slime)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		counter = Layer.lifeTime;
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		counter -= Time.deltaTime;
		
		if (counter <= 0) {
			SwitchState<SlimeDespawning>();
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
