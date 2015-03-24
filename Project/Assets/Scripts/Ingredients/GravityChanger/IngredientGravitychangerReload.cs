using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class IngredientGravitychangerReload : State {
	
	public float t;
    IngredientGravitychanger Layer {
    	get { return ((IngredientGravitychanger)layer); }
    }
	
	public override void OnEnter() {
		t = Layer.timeBetween;
	}
	
	public override void OnUpdate() {
		t -= Time.deltaTime;
		if(t <= 0){
			SwitchState<IngredientGravitychangerSpawning>();
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
