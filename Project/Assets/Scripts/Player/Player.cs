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
		CheckGrounded();
		
		controller.UpdateInputs();
		
		int hInput = 0;
		if (controller.getR)
			hInput ++;
		if (controller.getL)
			hInput --;
		
		Walk(hInput);
		
		if (controller.getJumpDown && grounded){
			Jump();
		}
	}
}
