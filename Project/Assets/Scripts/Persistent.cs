using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Persistent : MonoBehaviourExtended {

	void Awake() {
		DontDestroyOnLoad(gameObject);
		
		foreach (GameObject child in gameObject.GetChildrenRecursive()) {
			DontDestroyOnLoad(child);
		}
	}
}

