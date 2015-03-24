using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJumpJumping : State {
	
	public float jumpHeight = 10;
	public float runningStartBonus = 5f;
	
	ModelMoveMoving moving;
	
	ModelJump Layer {
		get { return ((ModelJump)layer); }
	}
	
	float JumpHeight {
		get{
			return jumpHeight + Mathf.Lerp(0, runningStartBonus, Mathf.Abs(Layer.rigidbody.velocity.x) / moving.speed);
		}
	}
	
	public override void OnAwake()
	{
		base.OnAwake();
		moving = GetComponent<ModelMoveMoving>();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.animator.Play(Layer.jumpingHash, 1);
		Layer.rigidbody.SetVelocity(JumpHeight, Axis.Y);
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.VerticalVelocity < 0) {
			SwitchState("Falling");
			Controller.DropSphere(transform.position, Color.magenta);
			return;
		}
	}
}
