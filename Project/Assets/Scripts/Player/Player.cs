using UnityEngine;
using System.Collections;

public class Player : Movable {

	// Use this for initialization
	protected override void Start () {
		base.Start();

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		controller.GetInputs();
		
		int hInput = 0;
		if (controller.getR)
			hInput ++;
		if (controller.getL)
			hInput --;
		
		Vel = Walk(hInput);
		
		if (controller.getJumpDown && grounded){
			Vel = Jump();
		}
	}
}
