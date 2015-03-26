using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class VineMoving : State {
	
	public float speed = 1;
	public float randomness = 0.5F;
	
	[Disable] public Vector3 position;
	[Disable] public float currentSpeed;
	
	Vine Layer {
		get { return ((Vine)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnAwake() {
		base.OnAwake();
		
		position = transform.position;
		currentSpeed = speed + speed * Random.Range(-randomness, randomness);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		transform.Translate(transform.right * currentSpeed, Axis.XY);
		
		if (!Layer.skinnedRenderer.isVisible) {
			Vector3 up = transform.up * randomness * 10;
			transform.position = position + new Vector3(Random.Range(-up.x, up.x), Random.Range(-up.y, up.y), Random.Range(-up.z, up.z));
			currentSpeed = speed + speed * Random.Range(-randomness, randomness);
		}
	}
}
