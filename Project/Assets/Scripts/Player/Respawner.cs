using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

	private PlayerStart[] startPos;
	
	// Use this for initialization
	void Start () {
//		Object[] starts = GameObject.FindObjectsOfType<PlayerStart>();
		startPos = FindObjectsOfType<PlayerStart>() as PlayerStart[];
	}
	
	void Respawn () {
		int i = Random.Range(0, 2);
		if (startPos[0] == null){
			startPos = FindObjectsOfType<PlayerStart>() as PlayerStart[];
		}
		transform.position = startPos[i].transform.position;
	}
	void OnTriggerEnter2D (Collider2D other){
		ThrillFloor floor = other.GetComponent<ThrillFloor>();
		if (floor != null){
			Respawn();
			Debug.Log("Respawning");
		}
	}
}
