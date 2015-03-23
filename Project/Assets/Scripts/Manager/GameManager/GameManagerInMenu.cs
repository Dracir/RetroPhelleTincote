using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameManagerInMenu : State {
	
    GameManager Layer {
    	get { return ((GameManager)layer); }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
