using UnityEngine;
using System.Collections;

public class GravityChangerParticule : Diable {

	public Vector2 gravity;
	
	Rigidbody2D body;
	
	
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	
	void FixedUpdate(){
		body.AddForce(gravity * Time.fixedDeltaTime);
		
		Vector2 v = body.velocity;
		float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		Rigidbody2D body = other.GetComponentInParent<Rigidbody2D>();
		if(body && body.transform.tag == "Player"){
			body.AddForce(gravity);
			isAlive = false;
		}
		
	}
}
