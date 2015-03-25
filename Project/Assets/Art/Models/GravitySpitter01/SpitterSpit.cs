using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpitterSpit : State {
	
	[Min] public int amount = 1;
	[Min] public float delay = 0.15F;
	[Min] public Vector3 randomness = new Vector3(0.1F, 0, 0);
	public Vector3 offset;
	
	[Disable] public float counter;
	
	Spitter Layer {
		get { return ((Spitter)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		counter = delay;
		Layer.animator.SetTrigger(Layer.spitHash);
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		counter -= Time.deltaTime;
		
		if (counter <= 0) {
			Spit();
			SwitchState<SpitterReload>();
			return;
		}
	}
	
	public void Spit() {
		for (int i = 0; i < amount; i++) {
			Vector3 position = transform.position + offset + new Vector3(Random.Range(-randomness.x, randomness.x), Random.Range(-randomness.y, randomness.y), Random.Range(-randomness.z, randomness.z));
			GameObject spit = Object.Instantiate(Layer.spitPrefab, position, Random.rotation) as GameObject;
			GravityChangerParticule gravityChanger = spit.GetComponent<GravityChangerParticule>();
		
			spit.GetComponent<Rigidbody2D>().SetVelocity(Layer.initialVelocity);
			gravityChanger.gravity = Layer.gravity;
			DiableManager.instance.addDiable(gravityChanger);
		}
	}
}
