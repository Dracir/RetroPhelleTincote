using UnityEngine;
using System.Collections.Generic;

namespace RickTools.MapLoader{
	[System.Serializable]
	public class MapLinker : ScriptableObject {
	
		public int savedTime = 0;
		public List<TiledTilesetData> tilesets = new List<TiledTilesetData>();
	
		public TiledTilesetData getOrCreateTileset(string name) {
			TiledTilesetData tileset = getTileset(name);
			if(tileset == null){
				tileset = new TiledTilesetData(name);
				tilesets.Add(tileset);
			}
			return tileset;
		}
		
		public TiledTilesetData getTileset(string name){
			foreach (var tileset in tilesets) {
				if(tileset.name == name){
					return tileset;
				}
			}	
			return null;
		}
	}
}