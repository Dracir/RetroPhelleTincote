using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJumpFalling : State {
	
    ModelJump Layer {
    	get { return ((ModelJump)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (Layer.controller.getJumpDown){
			RaycastHit2D[] rayInfo = Layer.RayCheck(Vector3.down, Mathf.Abs(Layer.raySettings.distance), 1.5f, transform.position, Layer.otherPLayer, 3);
			
			foreach (RaycastHit2D ray in rayInfo) {
				if (ray.point != Vector2.zero){
					SwitchState("Jumping");
				}
			}
		}
		
		if (Layer.Grounded) {
			SwitchState("Idle");
			if (Layer.machine.Debug){
				Controller.DropSphere(transform.position, Color.green);
			}
			return;
		}
	}
}
