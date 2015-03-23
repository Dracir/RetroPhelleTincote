using System.IO;
using UnityEditor;
using UnityEngine;

namespace RickTools.MapLoader{
	public class MapLoaderControler{
		
		public bool loadToPrefab ;
		public string prefabRoot = "Assets/Resources/Prefab/Map";
		
		public void loadFile(MapLinker linker, FileInfo file){
			MapLoader mapLoader = new MapLoader(linker);
			GameObject mapLoaded = mapLoader.loadFromFile(file);
			
			if(loadToPrefab){
				makeGameObjectAsPrefab(mapLoaded, mapLoaded.name);
			}
		}
		
		void makeGameObjectAsPrefab(GameObject gameObject, string name){
			PrefabUtility.CreatePrefab(prefabRoot + "/" + name + ".prefab", gameObject);
			Object.DestroyImmediate(gameObject);
		}
		
	}
}
