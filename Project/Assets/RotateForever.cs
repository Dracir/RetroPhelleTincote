using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class RotateForever : MonoBehaviourExtended {

	public float speed = 15;
	
	void Update() {
		transform.Rotate(speed, Axis.Z);
	}
}

