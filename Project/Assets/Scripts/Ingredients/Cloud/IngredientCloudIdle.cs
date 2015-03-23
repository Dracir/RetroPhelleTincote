using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class IngredientCloudIdle : State {
	
    IngredientCloud Layer {
    	get { return ((IngredientCloud)layer); }
    }
	
	public override void TriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Player"){
			Rigidbody2D body = collision.gameObject.GetComponentInParent<Rigidbody2D>();
			body.velocity *= Layer.velocityMultiplication;
			SwitchState<IngredientCloudPopOut>();
		}
	}
}
