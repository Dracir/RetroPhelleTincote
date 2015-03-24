using UnityEngine;

public class PlayerStart : MonoBehaviour {

	public int playerId;
	
	void OnDrawGizmos(){
		Gizmos.color = new Color(playerId / 2.0f, 1-playerId / 2.0f, 0, 1);
		Gizmos.DrawSphere (transform.position, 0.5f);
	}
}
	 
