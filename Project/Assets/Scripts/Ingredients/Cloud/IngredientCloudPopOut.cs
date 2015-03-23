using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class IngredientCloudPopOut : State {
	
    IngredientCloud Layer {
    	get { return ((IngredientCloud)layer); }
    }
	
	public override void OnEnter() {
		GetComponent<Animator>().SetTrigger("PopOut");
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
