using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GloudCooldown : State {
	
	public float time = 2;
	
	[Disable] public float counter;
	
	Gloud Layer {
		get { return ((Gloud)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		counter = time;
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		counter -= Time.deltaTime;
		
		if (counter <= 0) {
			SwitchState<GloudSpawning>();
			return;
		}
	}
}
