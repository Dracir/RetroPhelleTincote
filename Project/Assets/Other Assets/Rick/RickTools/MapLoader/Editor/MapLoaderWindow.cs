using UnityEngine;
using UnityEditor;
using RickEditor.Editor;

namespace RickTools.MapLoader{
	[System.Serializable]
	public class MapLoaderWindow : MapWindowBase {
		
		public bool multipleFileMod = true;
		
		
		[SerializeField]
		MultipleFilesPanel multipleFilePanel = new MultipleFilesPanel();
		[SerializeField]
		SingleFilePanel singleFilePanel = new SingleFilePanel();
		
		[MenuItem("Rick's Tools/Map Loader/Map Loader")]
		public static void Create() {
			CreateWindow<MapLoaderWindow>("Map Loader", new Vector2(275, 250));
		}

		protected override void onLinkerLoaded() {
			hideFlags = HideFlags.HideAndDontSave;
			multipleFilePanel.linker = linker;
			singleFilePanel.linker = linker;
		}
		
		
		protected override void showGUI() {
			multipleFileMod = RickEditorGUI.Toggle("Multiple file loading", multipleFileMod);
			if(multipleFileMod){
				multipleFilePanel.show();
			}else{
				singleFilePanel.show();
			}
			
		}
	}
}