using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameManagerPause : State {
	
    GameManager Layer {
    	get { return ((GameManager)layer); }
    }
	
	public override void OnUpdate(){
		if(Input.GetKeyDown(KeyCode.Return)){
			SwitchState<GameManagerInGame>();
		}
	}
	
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
