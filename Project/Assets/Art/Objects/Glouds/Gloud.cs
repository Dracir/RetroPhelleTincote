using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Gloud : StateLayer {
	
	public float damping = 0.5F;
	public float animationSpeed = 5;
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : GetComponentInChildren<SpriteRenderer>();
			_spriteRendererCached = true;
			return _spriteRenderer;
		}
	}
}
