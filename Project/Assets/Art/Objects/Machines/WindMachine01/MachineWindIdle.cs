using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.GeneralTools;

public class MachineWindIdle : State {
	
    MachineWind Layer {
    	get { return ((MachineWind)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		Layer.particleFX.Stop();
		Layer.oscillate.enabled = false;
		Layer.areaEffector.enabled = false;
		GetComponent<AudioSource>().Stop();
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
