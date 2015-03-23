using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Movable : MonoBehaviour {
	public LayerMask tempMask;
	public bool debug;
	const float gravity = 9.8f;
	
	public float walkForce = 1f;
	public float jumpForce = 10f;
	
	//rapid accessors
	protected Transform t; 
	protected SpriteRenderer sr;
	protected Collider2D col;
	protected Rigidbody2D rb;

	protected Vector2 Vel {
		set {
			rb.velocity = value;
		}
		get {
			return rb.velocity;
		}
	}
	
	protected virtual float JumpForce {
		get{
			return jumpForce;
		}
	}
	
	
	public bool grounded;
	protected float groundedRayCastDistance = 0.05f;
	
	protected Controller controller = new Controller();
	

	protected virtual void Start () {
		t = transform;
		sr = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
		
		
	}
	
	protected virtual void CheckGrounded () {
		float checkDistance = col.bounds.extents.y;
		checkDistance += Mathf.Max(-Vel.y * Time.deltaTime, groundedRayCastDistance);
		
		if (grounded ^ Vel.y <= 0){ 	// if check distance is smaller than it originally was,
												//  it means I'm moving up and don't want to check distance
			RaycastHit2D[] rayInfo = RayCheck(Vector3.down, checkDistance, col.bounds.size.x, col.bounds.center, tempMask, 6);
			bool collided = false;
			float smallestDistance = Mathf.Infinity;
			int smallestRay = -1;
			int i = 0;
			foreach (var item in rayInfo) {
				if (item.distance > 0){
					collided = true;
					if (item.distance < smallestDistance){
						smallestDistance = item.distance;
						smallestRay = i;
						if (debug){
							Debug.Log ("found something!");
						}
					}
				}
				i ++;
			}
			
			if (collided && !grounded){
				grounded = true;
				float heartToFoot = t.position.y - col.bounds.min.y;
				//for this we use bc.bounds 'cuz box is a step ahead
				t.position = new Vector3(t.position.x, rayInfo[smallestRay].point.y + heartToFoot, t.position.z);
				Vel = new Vector2(Vel.x, 0);
				
			} else if(!collided){
				grounded = false;
			}
		} else { Debug.Log("Never checked in the first place!");}
	}

	protected virtual void Jump () {
		
		rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
	}

	protected virtual void Walk (int input) {
		
		Vector2 direction = Vector2.right * input;
		
		rb.AddForce(direction * walkForce);
	}

	
	protected RaycastHit2D[] RayCheck (Vector3 direction, float distance, float breadth, Vector3 origin, int layermask, int number){
		
		RaycastHit2D[] rayHits = new RaycastHit2D[number];
		Vector3 side1 = origin + new Vector3(direction.y, -direction.x, 0).normalized * breadth / 2;
		Vector3 side2 = origin + new Vector3(-direction.y, direction.x, 0).normalized * breadth / 2;
		
		for (int i = 0; i < number; i ++){
			Vector3 o = Vector3.Lerp (side1, side2, (float)i / (float) (number - 1));

			rayHits[i] = Physics2D.Raycast(o, direction, distance, layermask);
			if (debug){
				Debug.DrawLine (o, o + (direction * distance));
			}
		}
		
		return rayHits;
	}
}
