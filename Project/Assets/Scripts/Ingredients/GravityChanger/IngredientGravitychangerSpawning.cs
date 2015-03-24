using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class IngredientGravitychangerSpawning : State {
	
	DiableManager diableManager;
	
	public override void OnAwake() {
		diableManager = DiableManager.instance;
	}
	
    IngredientGravitychanger Layer {
    	get { return ((IngredientGravitychanger)layer); }
    }
	
	public override void OnEnter() {
		GameObject newParticule = GameObjectExtend.createClone(Layer.gravityChangerParticule);
		GravityChangerParticule gcp = newParticule.GetComponent<GravityChangerParticule>();
		newParticule.transform.position = transform.position;
		
		newParticule.GetComponent<Rigidbody2D>().SetVelocity(Layer.initialVelocity);
		
		gcp.gravity = Layer.particuleGravity;
		SwitchState<IngredientGravitychangerReload>();
		
		DiableManager.instance.addDiable(gcp);
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
