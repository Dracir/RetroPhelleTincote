using System.Diagnostics;
using System.IO;
using UnityEngine;
using Magicolo.EditorTools;
using RickEditor.Editor;

namespace RickTools.MapLoader
{
	[System.Serializable]
	public class MultipleFilesPanel {
	
		public MapLinker linker;
		public string path = "";
		FileInfo[] selectedFiles;
		
		public bool loadToPrefab = false;
		public string prefabFolder = "Assets/Resources/Prefab/Map";
		
		MapLoaderControler mapLoaderControler = new MapLoaderControler();
		
		public void show() {
			showOptions();
			loadFilesInfo();
			showInformationSection();
			showButton();
		}
	
		void showOptions() {
			loadToPrefab = RickEditorGUI.Toggle("Load As Prefab", loadToPrefab);
			if (loadToPrefab) {
				prefabFolder = RickEditorGUI.FolderPath("Asset Prefab Folder", prefabFolder, RickEditorGUI.assetFolder);
			}
			CustomEditorBase.Separator();
			
			path = RickEditorGUI.FolderPath("Map folder path", path, RickEditorGUI.rootFolder);
		}
		
		void loadFilesInfo() {
			if (string.IsNullOrEmpty(path))
				return;
			
			var info = new DirectoryInfo(path);
			selectedFiles = info.GetFiles("*.tmx");
		}
		
		
		void showInformationSection() {
			if (selectedFiles == null)
				return;
			
			RickEditorGUI.Label("Number of files", selectedFiles.Length + "");
		}
	
		void showButton() {
			if (selectedFiles == null || selectedFiles.Length == 0) {
				GUI.enabled = false;
			}
			
			if (GUILayout.Button("Load All Map")) {
				mapLoaderControler.loadToPrefab = loadToPrefab;
				mapLoaderControler.prefabRoot = "Assets" + prefabFolder;
				int index = 0;
				Stopwatch stopWatch = new Stopwatch();
				stopWatch.Start();
				foreach (FileInfo file in selectedFiles) {
					mapLoaderControler.loadFile(linker, file);
					index++;
				}
				debuglog("Loaded Map" + index + " in " + stopWatch.ElapsedMilliseconds / 1000f + "s");
			}
			GUI.enabled = true;
		}
	
		void debuglog(string str) {
			UnityEngine.Debug.Log(str);
		}
	}
}
