using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJumpJumping : State {
	
	public float jumpHeight = 10;
	
	ModelJump Layer {
		get { return ((ModelJump)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.animator.Play(Layer.jumpingHash, 1);
		Layer.rigidbody.SetVelocity(jumpHeight, Axis.Y);
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.VerticalVelocity < 0) {
			SwitchState("Falling");
			return;
		}
	}
}
