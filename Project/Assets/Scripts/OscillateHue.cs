﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class OscillateHue : MonoBehaviourExtended {

	public float frequency = 0.5F;
	public float amplitude = 360;
	public float center = 180;
	
	[Disable] public Color hsv;
	
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
	
	void Awake(){
		hsv = material.color.ToHSV();
	}
	
	void Update() {
		hsv.r = center + amplitude * Mathf.Sin(frequency * Time.time);
		material.color = hsv.ToRGB();
	}
}

