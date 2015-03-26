using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GloudDespawning : State {
	
	Gloud Layer {
		get { return ((Gloud)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
		transform.localScale = Vector3.zero;
		Layer.spriteRenderer.SetColor(0, Channels.A);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		transform.ScaleLocalTowards(Vector3.one * 2, Layer.animationSpeed);
		Layer.spriteRenderer.FadeTowards(0, Layer.animationSpeed, Channels.A);
		
		if (Layer.spriteRenderer.GetColor().a <= 0.01F) {
			SwitchState<GloudCooldown>();
			return;
		}
	}
}
