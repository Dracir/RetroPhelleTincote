using System.Diagnostics;
using System.IO;
using UnityEngine;

using Magicolo.EditorTools;
using RickEditor.Editor;

namespace RickTools.MapLoader{
	[System.Serializable]
	public class SingleFilePanel {
	
		public MapLinker linker;
		public string filePath = "";
		FileInfo selectedFile;
		
		public bool loadToPrefab = false;
		public string prefabFolder = "Assets/Resources/Prefab/Map";
		
		MapLoaderControler mapLoaderControler = new MapLoaderControler();
		
		public void show(){
			showOptions();
			loadFilesInfo();
			showInformationSection();
			showButton();
		}
		
	
		void showOptions() {
			loadToPrefab = RickEditorGUI.Toggle("Load As Prefab", loadToPrefab);
			if(loadToPrefab){
				prefabFolder =  RickEditorGUI.FolderPath("Asset Prefab Folder", prefabFolder, RickEditorGUI.assetFolder);
			}
			CustomEditorBase.Separator();
			
			filePath =  RickEditorGUI.FilePath("Map ", filePath, RickEditorGUI.rootFolder, "tmx");
		}
		
		void loadFilesInfo() {
			if(string.IsNullOrEmpty(filePath)) return;
			selectedFile = new FileInfo(filePath + ".tmx");
		}
		
		void showInformationSection(){
			if(selectedFile == null) return;
		}
		
		void showButton(){
			if(selectedFile == null || selectedFile.Length == 0){
				GUI.enabled = false;
			}
			
			if (GUILayout.Button ("Load Map")) {
				mapLoaderControler.loadToPrefab = loadToPrefab;
				mapLoaderControler.prefabRoot = "Assets" + prefabFolder;
				
				Stopwatch stopWatch = new Stopwatch();
				stopWatch.Start();
				mapLoaderControler.loadFile(linker, selectedFile);
				stopWatch.Stop();
				
				debuglog("Loaded Map in " + stopWatch.ElapsedMilliseconds/1000f + "s");
			}
			GUI.enabled = true;
		}
		
		void debuglog(string str){
			UnityEngine.Debug.Log(str);
		}
	}
}