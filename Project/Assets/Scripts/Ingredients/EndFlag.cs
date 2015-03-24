using UnityEngine;
using System.Collections;

public class EndFlag : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		Rigidbody2D body = other.GetComponentInParent<Rigidbody2D>();
		if(body && body.transform.tag == "Player"){
			GameManager.instance.nextLevel();
		}
	}
}
