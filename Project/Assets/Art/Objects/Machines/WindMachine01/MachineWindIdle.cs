using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class MachineWindIdle : State {
	
    MachineWind Layer {
    	get { return ((MachineWind)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
