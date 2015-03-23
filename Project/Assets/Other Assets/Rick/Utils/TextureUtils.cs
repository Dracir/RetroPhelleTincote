using UnityEngine;
using System.Collections;

//TODO faire quelque chose quand la texture est le full sie du sprite


public static class TextureUtils {

	static int width, height;
	static Texture2D texture;
	static Color[] colors;
	
	public static Texture2D textureFromSprite(Sprite sprite){
		extractSprite(sprite);
		
		texture.SetPixels(colors);
        texture.Apply();
        return texture;
	}

	static void extractSprite(Sprite sprite){
		width = (int)sprite.rect.width;
		height = (int)sprite.rect.height;
		int x = (int)sprite.rect.x;
		int y = (int)sprite.rect.y;
		
		texture = new Texture2D(width,height);
        texture.hideFlags = HideFlags.DontSave;
        colors = sprite.texture.GetPixels(x, y, width, height );
	}
	
	public static Texture2D textureFromSpriteInverted(Sprite sprite){
		extractSprite(sprite);
		Color[] newColors = new Color[colors.Length];
		int offset = 0;
		
		for (int index = 0; index < colors.Length; index++) {
			Color color = colors[index];
			if((index + offset) % 2 == 0){
				newColors[index] = Color.red;
			}else{
				if(index % width == 0 || index % width == width - 1 || index < width || index > (width * (height - 1) )){
					newColors[index] = Color.red;
				}else{
					newColors[index] = color;
				}
			}
			if(index % (int)sprite.rect.width == 0){
				offset ++;
			}
		}
		texture.SetPixels(newColors);
		texture.Apply();
		return texture;
     }
	
	public static Texture2D textureFromSpriteGrayed(Sprite sprite){
		extractSprite(sprite);
		Color[] newColors = new Color[colors.Length];
		int offset = 0;
		
		for (int index = 0; index < colors.Length; index++) {
			Color color = colors[index];
			if((index + offset) % 2 == 0){
				newColors[index] = color;
			}else{
				newColors[index] = new Color(0.7f,0.7f,0.7f);
			
			}
			if(index % (int)sprite.rect.width == 0){
				offset ++;
			}
		}
		texture.SetPixels(newColors);
		texture.Apply();
		return texture;
     }

	static Color grayColor(Color color) {
		float gray = 0f;
		return new Color (Mathf.Max(color.r - gray,0) , Mathf.Max(color.g - gray,0), Mathf.Max(color.b - gray,0) );
	}

	static Color rededtColor(Color color){
		return new Color( 1f, color.g, color.b);
	}
	static Color InvertColor (Color color) {
		return new Color (1.0f-color.r, 1.0f-color.g, 1.0f-color.b);
	}
}
