using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameManagerInGame : State {
	
    GameManager Layer {
    	get { return ((GameManager)layer); }
    }
	
	public override void OnUpdate(){
		if(Input.GetKeyDown(KeyCode.F1)){
			Layer.nextLevel();
		}
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
