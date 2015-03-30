using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class OscillateHue : MonoBehaviourExtended {

	public float frequency = 0.5F;
	public float amplitude = 360;
	public float center = 180;
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : GetComponent<SpriteRenderer>();
			_spriteRendererCached = true;
			return _spriteRenderer;
		}
	}
	
	Material material {
		get {
			return spriteRenderer.material;
		}
	}
	
	void Update() {
//		Color hsv = material.color.ToHSV();
//		hsv.r = center + amplitude * Mathf.Sin(frequency * Time.time);
//		material.color = hsv.ToRGB();
		
		Color hsv = material.color.ToHSV();
		hsv.r += frequency;
		if (hsv.r > 1){
			hsv.r -= 1;
		}
		material.color = hsv.ToRGB();
	}
}

