using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class VineIdle : State {
	
    Vine Layer {
    	get { return ((Vine)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
