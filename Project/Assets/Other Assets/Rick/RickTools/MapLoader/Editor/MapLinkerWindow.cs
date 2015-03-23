using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

using Magicolo.EditorTools;
using RickEditor.Editor;

namespace RickTools.MapLoader{
	[System.Serializable]
	public class MapLinkerWindow : MapWindowBase {
	
		public string tilesetPath = "";
		
		public int selectedTilesetIndex;
		public string[] tilsetChoicesText;
		public TiledTilesetData selectedTileset;
		
		public TiledTileData selectedTile;
		
		public List<Texture2D> buttonsIcons 		= new List<Texture2D>();
		public List<Texture2D> buttonsIconsInverted = new List<Texture2D>();
		public List<Texture2D> buttonsIconsGray 	= new List<Texture2D>();
		
		
		[MenuItem("Rick's Tools/Map Loader/Linker")]
		public static void Create() {
			CreateWindow<MapLinkerWindow>("Map Linker", new Vector2(275, 250));
		}
	
	
		/*protected void OnEnable(){
			if(linker.tilesets.Count > 0 && selectedTileset == null){
				selectedTileset = linker.tilesets[0];
			}
			if(selectedTileset != null){
				loadTilesetImage(selectedTileset);
			}
		}*/
		
		
		protected override void onLinkerLoaded(){
			if(linker.tilesets.Count > 0 && selectedTileset == null){
				selectedTileset = linker.tilesets[0];
			}
			if(selectedTileset != null){
				loadTilesetImage(selectedTileset);
			}
		}
		
		protected override void showGUI() {
			showAnalyseTilset();
			if(linker.tilesets.Count > 0){
				CustomEditorBase.Separator();
				showTilesetSelector();
				if(selectedTileset != null){
					showTileset();
					if(selectedTile != null){
						CustomEditorBase.Separator();
						showTile();
					}
				}
				
			}else{
				RickEditorGUI.Label("", "No Tileset Loaded :(");
			}
			
			
		}
	
		
		
		void showAnalyseTilset() {
			GUI.changed = false;
			tilesetPath = RickEditorGUI.FilePath("Load Tileset", tilesetPath, RickEditorGUI.rootFolder, "tsx", true);
			if(GUI.changed){
				loadTileset(tilesetPath);
			}
		}
	
		void showTilesetSelector() {
			tilsetChoicesText = new string[linker.tilesets.Count];
			for (int index = 0; index < linker.tilesets.Count; index++) {
				tilsetChoicesText[index] = linker.tilesets[index].name;
			}
			GUI.changed = false;
			selectedTilesetIndex = RickEditorGUI.Popup("Editing Tileset", selectedTilesetIndex,tilsetChoicesText);
			if(GUI.changed){
				selectedTileset = linker.tilesets[selectedTilesetIndex];
				loadTilesetImage(selectedTileset);
			}
		}
		
		void showTileset() {
			for (int row = 0; row < selectedTileset.nbTileY; row++) {
				EditorGUILayout.BeginHorizontal();
				for (int col = 0; col < selectedTileset.nbTileX; col++) {
					TiledTileData tile = selectedTileset.getTile(row,col);
					showTileButton(tile);
				}
				EditorGUILayout.EndHorizontal();
			}
		}
	
		void loadTilesetImage(TiledTilesetData tileset) {
			buttonsIcons = new List<Texture2D>();
			Sprite[] sprites = Resources.LoadAll<Sprite>(tileset.imagePath);
			
			killAllTexturesFrom(buttonsIcons);
			killAllTexturesFrom(buttonsIconsGray);
			killAllTexturesFrom(buttonsIconsInverted);
			foreach (var sprite in sprites) {
				buttonsIcons.Add( TextureUtils.textureFromSprite(sprite) );
				buttonsIconsGray.Add( TextureUtils.textureFromSpriteGrayed(sprite) );
				buttonsIconsInverted.Add( TextureUtils.textureFromSpriteInverted(sprite) );
			}
		}
	
		void killAllTexturesFrom(List<Texture2D> textureList){
			foreach (var texture in textureList) {
				DestroyImmediate(texture);
			}
			textureList.Clear();
		}
		
		
		
		void showTileButton(TiledTileData tile) {
			if(buttonsIcons != null && buttonsIcons.Count > tile.id){
				GUIStyle labelStyle;
				if(selectedTile != null && tile.id == selectedTile.id){
					labelStyle = new GUIStyle("TL SelectionButton PreDropGlow");
					labelStyle.border = new RectOffset(10,10,10,10);
				}else{
					labelStyle = GUI.skin.label;
					labelStyle.border = new RectOffset(0,0,0,0);
				}
				labelStyle.margin = new RectOffset(1,1,1,1);
				labelStyle.padding = new RectOffset(0,0,0,0);
				labelStyle.stretchWidth = false;
				
				Texture2D texture = getTextureFor(tile);
				if (GUILayout.Button(texture,labelStyle)) {
					buttonClick(tile.id);
				}
			}else{
				if (GUILayout.Button(tile.id + "")) {
					buttonClick(tile.id);
				}
			}
			
			
		}
	
		Texture2D getTextureFor(TiledTileData tile) {
			if(selectedTile != null && tile.id == selectedTile.id){
				return buttonsIcons[tile.id];
			}else{
				if(tile.prefab != null){
					return buttonsIconsGray[tile.id];
				}else{
					return buttonsIconsInverted[tile.id];
				}
			}
			
			
		
		}
		void buttonClick(int id) {
			selectedTile = selectedTileset.tiles[id];
		}
	
		
		void showTile() {
			RickEditorGUI.Label("Tile id",selectedTile.id + "");
			
			GUI.changed = false;
			selectedTile.prefab = (GameObject) RickEditorGUI.ObjectField("Prefab", selectedTile.prefab, typeof(GameObject), false);
			dataChanged |= GUI.changed;
		}
		
		
		void loadTileset(string filePath) {
			TilesetLoader tilesetLoader = new TilesetLoader(linker);
			tilesetLoader.loadFromFile(filePath);
			linker.savedTime ++;
			dataChanged = true;
		}
	}
}