using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpitterReload : State {
	
	[Min] public float reloadTime = 0.1F;
	
	[Disable] public float counter;
	
	Spitter Layer {
		get { return ((Spitter)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		counter = reloadTime;
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		counter -= Time.deltaTime;
		
		if (counter <= 0) {
			SwitchState<SpitterSpit>();
		}
	}
}
