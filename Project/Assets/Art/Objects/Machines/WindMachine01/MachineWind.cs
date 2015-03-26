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
	
	bool _areaEffectorCached;
	AreaEffector2D _areaEffector;
	public AreaEffector2D areaEffector { 
		get { 
			_areaEffector = _areaEffectorCached ? _areaEffector : GetComponent<AreaEffector2D>();
			_areaEffectorCached = true;
			return _areaEffector;
		}
	}
}
