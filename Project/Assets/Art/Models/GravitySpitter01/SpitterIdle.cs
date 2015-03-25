using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpitterIdle : State {
	
    Spitter Layer {
    	get { return ((Spitter)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
