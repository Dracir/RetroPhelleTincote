using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJump : StateLayer {
	
	public KeyCode jumpKey1 = KeyCode.UpArrow;
	public KeyCode jumpKey2 = KeyCode.JoystickButton0;
	public RaySettings raySettings;
	
	[HideInInspector]
	public ModelMove mover;
	public Controller controller {
		get{
			return mover.controller;
		}
	}
	
	[SerializeField, Disable] bool grounded;
	public bool Grounded {
		get {
			return grounded;
		}
		set {
			if (grounded != value) {
				grounded = value;
				animator.SetBool(groundedHash, grounded);
			}
		}
	}

	[SerializeField, Disable] float verticalVelocity;
	public float VerticalVelocity {
		get {
			return verticalVelocity;
		}
		set {
			if (verticalVelocity != value) {
				verticalVelocity = value;
				animator.SetFloat(verticalVelocityHash, verticalVelocity);
			}
		}
	}
	
	#region Hashes
	[Disable] public int groundedHash = Animator.StringToHash("Grounded");
	[Disable] public int jumpingHash = Animator.StringToHash("Jumping");
	[Disable] public int verticalVelocityHash = Animator.StringToHash("VerticalVelocity");
	#endregion
	
	#region Cached Components
	bool _animatorCached;
	Animator _animator;
	public Animator animator { 
		get { 
			_animator = _animatorCached ? _animator : GetComponent<Animator>();
			_animatorCached = true;
			return _animator;
		}
	}
	
	bool _rigidbodyCached;
	Rigidbody2D _rigidbody;
	new public Rigidbody2D rigidbody { 
		get { 
			_rigidbody = _rigidbodyCached ? _rigidbody : GetComponent<Rigidbody2D>();
			_rigidbodyCached = true;
			return _rigidbody;
		}
	}
	#endregion
	
	public override void OnAwake () {
		base.OnAwake();
		mover = GetComponent<ModelMove>();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		controller.UpdateInputs();
		Grounded = CastRays();
		VerticalVelocity = rigidbody.velocity.y;
	}
	
	public bool CastRays() {
		float adjustedDistance = raySettings.distance / Mathf.Cos(raySettings.spread * Mathf.Deg2Rad);
		bool hit = false;
		
		hit |= Physics2D.Raycast(transform.position + raySettings.offset, -transform.up, raySettings.distance, raySettings.layerMask);
		hit |= Physics2D.Raycast(transform.position + raySettings.offset, -transform.up.Rotate(raySettings.spread), adjustedDistance, raySettings.layerMask);
		hit |= Physics2D.Raycast(transform.position + raySettings.offset, -transform.up.Rotate(-raySettings.spread), adjustedDistance, raySettings.layerMask);
		
		if (machine.Debug) {
			Debug.DrawRay(transform.position + raySettings.offset, -transform.up * raySettings.distance, Color.green);
			Debug.DrawRay(transform.position + raySettings.offset, -transform.up.Rotate(raySettings.spread) * adjustedDistance, Color.green);
			Debug.DrawRay(transform.position + raySettings.offset, -transform.up.Rotate(-raySettings.spread) * adjustedDistance, Color.green);
		}
		
		return hit;
	}
	
	[System.Serializable]
	public class RaySettings {
		
		public Vector3 offset;
		[Range(-90, 90)] public float spread = 30;
		[Min] public float distance = 1;
		public LayerMask layerMask;
	}
}
