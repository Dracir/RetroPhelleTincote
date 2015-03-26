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
		GameManager.instance.relocatePlayerToStart(gameObject);
	}
	void OnTriggerEnter2D (Collider2D other){
		ThrillFloor floor = other.GetComponent<ThrillFloor>();
		if (floor != null){
			SendMessage("Respawn");
		}
	}
}
