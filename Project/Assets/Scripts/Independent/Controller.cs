using UnityEngine;
using System.Collections;


public class Controller {
	public bool getRun;
	public bool getRunDown;
	public bool getRunUp;
	
	public float lastRunDownTime;
	private bool getRunLast;
	public bool isSpammingRun;
	private readonly float spamResetTimer = 0.3f;
	
	public bool getJump;
	public bool getJumpDown;
	public bool getJumpUp;
	
	private bool getJumpLast;
	private float lastJumpTime;
	private float jumpInputLeeway = 0.2f;
	
	public bool getL;
	public bool getLUp;
	public bool getLDown;
	
	public bool getR;
	public bool getRUp;
	public bool getRDown;
	
	public bool getD;
	public bool getDDown;
	public bool getDUp;
	
	public bool getU;
	public bool getUUp;
	public bool getUDown;
	
	public bool doubleTap;
	public bool aboutFace;

	public bool locked;
	
	public float hAxis;
	public float vAxis;
	
	float axisMinimum = 0.1f;
	
	public class ButtonNames {
		public string hAxis = "Horizontal";
		public string vAxis = "Vertical";
		public string jump = "Jump";
		public string run = "Run";
		
		public ButtonNames (int number){
			if (number > 1){
				hAxis += number.ToString();
				vAxis += number.ToString();
				jump += number.ToString();
				run += number.ToString();
			}
		}
	}
	
	private ButtonNames butts;
	private float hAxisLast = 0.0f;
	private float vAxisLast = 0.0f;
	private float getLLastTime = 0f;
	private float getRLastTime = 0f;
	private float doubleTapTime = 0.188f;
	
//	private static KeyCode jump = KeyCode;
//	private static KeyCode run;
	
	public Controller(int playerNo){
		butts = new ButtonNames(playerNo);
		lastJumpTime= -jumpInputLeeway ;
	}
	
	public Controller() : this(0) {
		
	}
	
	public void UpdateInputs(){
		doubleTap = false;
		if (locked) return;
		
		//get run button
		
//		getRun = Input.GetButton(butts.run) || Input.GetKey(run);
//		getRunDown = Input.GetButtonDown(butts.run) || Input.GetKeyDown(run);
//		getRunUp = Input.GetButtonUp(butts.run) || Input.GetKeyUp(run);
		getRun = Input.GetButton(butts.run);
		getRunDown = Input.GetButtonDown(butts.run);
		getRunUp = Input.GetButtonUp(butts.run);
		
		if (getRunDown){
			isSpammingRun = Time.time - lastRunDownTime < spamResetTimer;
			
			lastRunDownTime = Time.time;
			
		}
		//get jump button
		
//		getJump = Input.GetButton(butts.jump) || Input.GetKey(jump);
//		getJumpUp = Input.GetButtonUp(butts.jump) || Input.GetKeyUp(jump);
		
		getJump = Input.GetButton(butts.jump);
		getJumpUp = Input.GetButtonUp(butts.jump);
		
		if (getJump && !getJumpLast){
			lastJumpTime = Time.time;
		}
		
		getJumpDown = Time.time - lastJumpTime < jumpInputLeeway;
		Logger.Log(Time.time - lastJumpTime , lastJumpTime);
		
		getJumpLast = getJump;
		
		//get H axis
		hAxis = Input.GetAxis(butts.hAxis);
		getL = hAxis < -axisMinimum;
		getR = hAxis > axisMinimum;
		
		getLDown = (hAxisLast < axisMinimum && hAxisLast > -axisMinimum) && getL;
		getRDown = (hAxisLast < axisMinimum && hAxisLast > -axisMinimum) && getR;
		
		getLUp = hAxisLast < -axisMinimum && !getL;
		getRUp = hAxisLast > axisMinimum && !getR;
		
		if (getLDown){						//check for doubleTap
			if (Time.time - getLLastTime < doubleTapTime){
				doubleTap = true;
			}
			getLLastTime = Time.time;
		}
		
		if (getRDown){
			if (Time.time - getRLastTime < doubleTapTime){
				doubleTap = true;
			}
			getRLastTime = Time.time;
		}
		hAxisLast = hAxis;
		
		VerticalInputs();
	}
	
	public void CursorInput(){
		VerticalInputs();
		
	}
	
	private void VerticalInputs(){
		vAxis = Input.GetAxis(butts.vAxis);
		
		getD = vAxis < -axisMinimum;
		getU = vAxis > axisMinimum;
		
		getDDown = (vAxisLast < axisMinimum && vAxisLast > -axisMinimum) && getD;
		getUDown = (vAxisLast < axisMinimum && vAxisLast > -axisMinimum) && getU;
		
		getDUp = vAxisLast < -axisMinimum && !getD;
		getUUp = vAxisLast > axisMinimum && !getU;
		vAxisLast = vAxis;
	}
	
	public void ResetJumpInput(){
		lastJumpTime = 0;
		getJumpDown = false;
	}
	
	public static void DropSphere (Vector3 position, Color colour) {
		/*GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = position;
		sphere.GetComponent<MeshRenderer>().material.color = colour;
		sphere.transform.localScale = Vector3.one * 0.1f;
		GameObject.Destroy(sphere, 25f);*/
	}
}