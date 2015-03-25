using UnityEngine;
using System.Collections;

public class ThrillFloor : MonoBehaviour {
	
//	private int Layer {
//		get{
//			return 1 << LayerMask.NameToLayer("Walls");
//		}
//	}
	
	public LayerMask mask;
	EdgeCollider2D col;
	// Use this for initialization
	void Start () {
		col = GetComponent<EdgeCollider2D>();
		if (col == null){
			col = gameObject.AddComponent<EdgeCollider2D>();
		}
		
		ReformEdge();
		
		col.isTrigger = true;
	}
	
	void ReformEdge () {
		RaycastHit2D left = Physics2D.Raycast(transform.position, -Vector2.right);
		RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right);
		Vector2[] points = { left.point - (Vector2)transform.position, right.point - (Vector2)transform.position};
		
		col.points = points;
	}
	
	// Update is called once per frame
	void Update () {
		if (col.points[0] == Vector2.zero){
			ReformEdge();
		}
	}
	
	void OnTriggerEnter2D (Collider2D other){
		other.SendMessage("Respawn", SendMessageOptions.DontRequireReceiver);
	}
	
	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0.2f);
		
	}
}
