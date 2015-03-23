using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Magicolo {
	public static class MaterialExtensions {

		public static void SetColor(this Material material, Color color, Channels channels) {
			Color oldColor = material.color;
			
			material.color = new Color(channels.Contains(Channels.R) ? color.r : oldColor.r, channels.Contains(Channels.G) ? color.g : oldColor.g, channels.Contains(Channels.B) ? color.b : oldColor.b, channels.Contains(Channels.A) ? color.a : oldColor.a);
		}
		
		public static void SetColor(this Material material, float color, Channels channels) {
			material.SetColor(new Color(color, color, color, color), channels);
		}
	}
}

