using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class MachineWindBlow : State {
	
	MachineWind Layer {
		get { return ((MachineWind)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		UpdateRotation();
		Layer.particleFX.Play();
		Layer.oscillate.enabled = true;
		Layer.areaEffector.enabled = true;
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (transform.hasChanged) {
			UpdateRotation();
			transform.hasChanged = false;
		}
	}
	
	public void UpdateRotation() {
		float angle = transform.eulerAngles.z;
			
		Layer.particleFX.startRotation = -transform.eulerAngles.z * Mathf.Deg2Rad;
		Layer.areaEffector.forceDirection = angle + 90;
		transform.hasChanged = false;
	}
}
