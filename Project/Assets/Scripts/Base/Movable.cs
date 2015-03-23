using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Movable : MonoBehaviour {
	public LayerMask tempMask;
	public bool debug;
	const float gravity = 9.8f;
	
	public float walkForce = 1f;
	public float jumpForce = 10f;
	
	//rapid accessors
	protected Transform t; 
	protected SpriteRenderer sr;
	protected BoxCollider2D bc;
	protected Rigidbody2D rb;

	protected Vector2 Vel {
		set {
			rb.velocity = value;
		}
		get {
			return rb.velocity;
		}
	}
	
	
	
	protected bool grounded;
	
	protected Controller controller = new Controller();
	

	protected virtual void Start () {
		t = transform;
		sr = GetComponent<SpriteRenderer>();
		bc = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
		
		
	}


	protected virtual Vector2 Jump () {
		
		return Vector2.zero;
	}

	protected virtual Vector2 Walk (int input) {
		float xSpeed = rb.velocity.x;
		return new Vector2(xSpeed, Vel.y);
	}

	
}
