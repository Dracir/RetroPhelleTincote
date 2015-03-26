using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class MachineWind : StateLayer {
	
	[SerializeField, PropertyField]
	int density = 100;
	public int Density {
		get {
			return density;
		}
		set {
			density = value;
			particleFX.emissionRate = density;
		}
	}
	
	public ParticleSystem particleFX;
	
	bool _boxColliderCached;
	BoxCollider2D _boxCollider;
	public BoxCollider2D boxCollider { 
		get { 
			_boxCollider = _boxColliderCached ? _boxCollider : GetComponent<BoxCollider2D>();
			_boxColliderCached = true;
			return _boxCollider;
		}
	}
	
	bool _areaEffectorCached;
	AreaEffector2D _areaEffector;
	public AreaEffector2D areaEffector { 
		get { 
			_areaEffector = _areaEffectorCached ? _areaEffector : GetComponent<AreaEffector2D>();
			_areaEffectorCached = true;
			return _areaEffector;
		}
	}
	
	bool _oscillateCached;
	SmoothOscillate _oscillate;
	public SmoothOscillate oscillate { 
		get { 
			_oscillate = _oscillateCached ? _oscillate : GetComponentInChildren<SmoothOscillate>();
			_oscillateCached = true;
			return _oscillate;
		}
	}

	public override void OnAwake() {
		base.OnAwake();
		
		LayerMask layerMask = new LayerMask().AddToMask("Walls");
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 100, layerMask);
		
		if (hit.collider != null) {
			float distance = Vector2.Distance(particleFX.transform.position, hit.point);
			
			particleFX.startLifetime = distance / 5;
			boxCollider.size = new Vector2(boxCollider.size.x, distance);
			boxCollider.offset = new Vector2(0, distance / 2 + 0.75F);
		}
	}
}
