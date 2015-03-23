using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class IngredientCloudIdle : State {
	
    IngredientCloud Layer {
    	get { return ((IngredientCloud)layer); }
    }
	
	public override void CollisionEnter2D(Collision2D collision) {
		if(collision.gameObject.tag == "Player"){
			Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
			body.velocity *= Layer.velocityMultiplication;
			SwitchState<IngredientCloudPopOut>();
		}
	}
}
