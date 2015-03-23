using UnityEngine;
using UnityEditor;

using Magicolo.EditorTools;
using RickEditor.Editor;

namespace RickTools.MapLoader{
	public abstract class MapWindowBase : CustomWindowBase {
	
		public MapLinker linker;
		public string linkerPath = "assets";
		public bool dataChanged = true;
	
		
		void OnGUI() {
			if(linker == null){
				showSelectOrCreateLinkerPanel();
			}else{
				EditorGUI.BeginChangeCheck();
				showGUI();
				if (EditorGUI.EndChangeCheck() || dataChanged){
					EditorUtility.SetDirty(linker);
					dataChanged = false;
				} 
			}
		}
	
		protected abstract void showGUI();
		
		
		void showSelectOrCreateLinkerPanel() {
			GUI.changed = false;
			
			
			linkerPath = RickEditorGUI.FilePath("Load Linker", linkerPath, RickEditorGUI.assetFolder, "asset", true);
			if(GUI.changed){
				loadNewLinker();
				onLinkerLoaded();
			}
			CustomEditorBase.Separator();
			if (GUILayout.Button ("CreateNewLinker")) {
				createNewLinker();
			}
		}
	
		protected abstract void onLinkerLoaded();
		
		void loadNewLinker() {
			MapLinker loadedLinker = (MapLinker)AssetDatabase.LoadAssetAtPath("Assets" + linkerPath, typeof(MapLinker));
			if(loadedLinker != null){
				linker = loadedLinker;
			}else{
				Debug.LogError("Selected file not a Linker!");
			}
		}
	
		void createNewLinker() {
			MapLinker newLinker = (MapLinker)ScriptableObject.CreateInstance("MapLinker");
			AssetDatabase.CreateAsset(newLinker, "Assets/linker.asset");
		}
		
	}
}