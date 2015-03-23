using System.IO;
using UnityEngine;
using System.Collections.Generic;
using Rick.TiledMapLoader;

namespace RickTools.MapLoader{
	[System.Serializable]
	public class MapLoader : TiledMapLoader {
	
		public MapLinker linker;
		MapLoaderStatistics statistics;
		
		public List<TiledTileData> tiles = new List<TiledTileData>();
		
		public Transform parent;
		
		public MapLoader(MapLinker linker){
			if(linker == null) Debug.LogError("The linker provided is null");
			this.linker = linker;
		}
		
		public GameObject loadFromFile(FileInfo file){
			statistics = new MapLoaderStatistics();
			string name = file.Name.Split(new char[]{'.'})[0];
			createParent(name);
			tiles.Clear();
			
	        base.loadFromFile(file.FullName);
	        statistics.showToConsole();
	        return parent.gameObject;
		}
	
		void createParent(string name){
			GameObject parentGo = new GameObject(name);
			parent = parentGo.transform;
		}
		
		protected override void addExternalTileset(int firstGridId, string source){
			if(linker == null) Debug.LogError("The linker provided is null");
			var tileset = linker.getTileset(source);
			if(tileset == null){
				Debug.LogError("The tileset \"" + source + "\" is unknown to the linker.");
			}else{
				tiles.AddRange(tileset.tiles);
			}
		}
	
		protected override void afterAll() {
			
		}
		
		protected override void afterMapAttributesLoaded() {
			
		}
		
		protected override void addObject(string objectGroupName, int x, int y, Dictionary<string, string> properties) {
			
		}
		
		protected override void addLayer(string layerName, int width, int height, Dictionary<string, string> properties) {
			
		}
		
		protected override void addTile(int x, int y, int id) {
			TiledTileData tileData = tiles[id-1];
			
			if(tileData == null || tileData.id == 0){
				debugLog("yo c null ton tileData ou le ID");
			}else if(tileData.prefab == null){
				statistics.addWarning("Tile " + tileData.id + " missing, used times");
			}else{
				//debugLog("fake on rajoute " + tileData.prefab.name + " (" + (id-1) + ")");
				
				Vector3 position = new Vector3(x,y);
				GameObject original = tileData.prefab;
				GameObject go = (GameObject)Object.Instantiate(original);
				go.name = original.name;
				go.transform.position = position;
				go.transform.parent = parent;
			}
			
		}
		
		protected override void loadMapProperty(Dictionary<string, string> properties) {
			
		}
		
	}
}
