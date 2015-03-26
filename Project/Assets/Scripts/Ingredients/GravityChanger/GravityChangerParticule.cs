using UnityEngine;
using System.Collections;
using Magicolo;

public class GravityChangerParticule : Diable {

	public Vector2 gravity;
	public float maxSpeed;
	
	Rigidbody2D body;
	
	void Start() {
		body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
		body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
		body.AddForce(gravity * Time.fixedDeltaTime, ForceMode2D.Impulse);
	}
	
	void Update() {
		faceFoward();
	}

	void faceFoward() {
		Vector2 v = body.velocity;
		float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Specials")) {
			return;
		}
		
		Rigidbody2D playerRigidbody = other.GetComponentInParent<Rigidbody2D>();
		
		if (playerRigidbody && playerRigidbody.transform.tag == "Player") {
			playerRigidbody.SetVelocity(gravity);
		}
		
		isAlive = false;
		
	}
}
