using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GloudSpawning : State {

	[Disable] public Vector3 scale;
	[Disable] public Vector3 position;
	
	Gloud Layer {
		get { return ((Gloud)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		transform.position = position;
		transform.localScale = Vector3.zero;
		Layer.spriteRenderer.SetColor(0, Channels.A);
		Layer.PlaySound(Layer.reformSound);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		transform.localScale = scale;
		Layer.spriteRenderer.SetColor(1, Channels.A);
	}
	
	public override void OnAwake() {
		base.OnAwake();
		
		position = transform.position;
		scale = transform.localScale;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		transform.ScaleLocalTowards(scale, Layer.animationSpeed);
		Layer.spriteRenderer.FadeTowards(1, Layer.animationSpeed, Channels.A);
		
		if (Layer.spriteRenderer.GetColor().a >= 0.99F) {
			SwitchState<GloudIdle>();
			return;
		}
	}
}
