using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelMoveMoving : State {
	
	[Min] public float speed = 3;
	[Min] public float acceleration = 100;
	[Min] public float airAccelerationModifier = 0.4f;
	[Min] public float inputPower = 1;
	[Disable] public float currentSpeed;
	
	float dropBallTimer;
	float dropBallTiming = 0.5f;
	
	ModelJump jumper;
	
	float Acceleration {
		get{
			return acceleration * (jumper.Grounded? 1 : airAccelerationModifier);
		}
	}
	
    ModelMove Layer {
    	get { return ((ModelMove)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnAwake()
	{
		base.OnStart();
		jumper = GetComponent<ModelJump>();
		if (!jumper){
			Debug.Log("Don't find a jumper from modelmovemoving....");
		}
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.AbsHorizontalAxis <= Layer.moveThreshold) {
			SwitchState("Idle");
		}
		if (Layer.machine.Debug){
			float hAxis = Layer.controller.hAxis;
			float deadzone = 0.001f;
			if (hAxis > deadzone || hAxis < -deadzone){
				dropBallTimer += Time.deltaTime;
				if (dropBallTimer > dropBallTiming){
					Controller.DropSphere(transform.position, Color.blue);
					dropBallTimer = 0;
				}
			} else {
				dropBallTimer = 0;
			}
		}
	}
	
	
	
	public override void OnFixedUpdate() {
		float hAxis = Layer.controller.hAxis;
		currentSpeed = hAxis.PowSign(inputPower) * speed;
		Layer.rigidbody.AccelerateTowards(currentSpeed, Acceleration, Axis.X);
		
	}
}
