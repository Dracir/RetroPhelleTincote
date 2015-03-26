using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Vine : StateLayer {
	
	bool _skinnedRendererCached;
	SkinnedMeshRenderer _skinnedRenderer;
	public SkinnedMeshRenderer skinnedRenderer { 
		get { 
			_skinnedRenderer = _skinnedRendererCached ? _skinnedRenderer : GetComponentInChildren<SkinnedMeshRenderer>();
			_skinnedRendererCached = true;
			return _skinnedRenderer;
		}
	}
}
