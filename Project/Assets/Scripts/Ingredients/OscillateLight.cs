using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class OscillateLight : MonoBehaviourExtended {

	public float frequency = 1;
	public float amplitude = 0.5F;
	public float center = 0.5F;
	
	bool _lightCached;
	Light _light;
	new public Light light { 
		get { 
			_light = _lightCached ? _light : GetComponent<Light>();
			_lightCached = true;
			return _light;
		}
	}
	
	void Update() {
		light.intensity = center + amplitude * Mathf.Sin(frequency * Time.time + GetInstanceID() / 1000);
	}
}

