using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJumpIdle : State {
	
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
		
		if (Layer.controller.getJumpDown) {
			Debug.Log("JUMP");
			SwitchState("Jumping");
			if (Layer.machine.Debug){
				Controller.DropSphere(transform.position, Color.yellow);
			}
			return;
		}
		
		if (!Layer.Grounded) {
			SwitchState("Falling");
			return;
		}
	}
}
