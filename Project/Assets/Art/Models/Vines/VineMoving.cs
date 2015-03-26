using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class VineMoving : State {
	
	public float speed = 1;
	
	[Disable] public Vector3 position;
	
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
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		transform.Translate(transform.right * speed, Axis.XY);
		
		if (!Layer.skinnedRenderer.isVisible) {
			transform.position = position;
		}
	}
}
