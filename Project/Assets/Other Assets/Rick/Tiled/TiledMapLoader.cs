using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace Rick.TiledMapLoader{
	public abstract class TiledMapLoader : TiledLoader{
		
		public bool callAddEmptyTiles = true;
		
		protected int mapWidth;
		protected int mapHeight;
		protected Color32 backgroundColor;
		
		protected Dictionary<Int32,Dictionary<String,String>> tilesetTiles = new Dictionary<int, Dictionary<string, string>>();
		
		XDocument document;
		XElement mapElement;
		
		
		public void loadFromFile(string fileName){
			string text = File.ReadAllText(fileName);
			clearEverything();
	        loadLevel(text);
		}
	
		void clearEverything(){
			tilesetTiles.Clear();
			backgroundColor = new Color32(0,0,0,0);
		}
		void loadLevel(string levelFileContent){
			document = XDocument.Parse(levelFileContent);
			mapElement = document.Element("map");
			loadMapAttributes();
	        loadMapProperties();
	        loadTilesets();
	        loadLevelsLayers();
	        loadLevelsObjectGroup();
	        afterAll();
		}
		
		
		protected abstract void afterAll();
	
		
		void loadMapAttributes(){
			mapWidth  = parseInt(mapElement.Attribute("width").Value);
			mapHeight = parseInt(mapElement.Attribute("height").Value);
			if(mapElement.Attribute("backgroundcolor") != null){
				string colorInHex = mapElement.Attribute("backgroundcolor").Value.Substring(1);
				backgroundColor = ColorUtils.HexToColor(colorInHex);
			}
			afterMapAttributesLoaded();
		}
		protected abstract void afterMapAttributesLoaded();
	
		void loadTilesets(){
			var tilesets = mapElement.Elements("tileset");
			foreach (var tileset in tilesets) {
				if(tileset.Attribute("source") != null){
					loadExternalTileset(tileset);
				}else{
					loadInternalTileset(tileset);
				}
			}
		}
	
		void loadExternalTileset(XElement tileset) {
			int firstGridId = parseInt(tileset.Attribute("firstgid").Value);
			string source = tileset.Attribute("source").Value;
			source = source.Substring(0,source.Length - 4);
			addExternalTileset(firstGridId, source);
		}
		
		protected virtual void addExternalTileset(int firstGridId,string source){
			
		}
		
		
		void loadInternalTileset(XElement tileset) {
			foreach (var tile in tileset.Elements("tile")) {
					int id = this.parseInt(tile.Attribute("id").Value);
					
					Dictionary<string, string> dictionnary = makePropertiesDictionary(tile.Element("properties"));
					tilesetTiles.Add(id, dictionnary);
				}
		}
		
		
		void loadLevelsObjectGroup(){
			 var objGroups = document.Elements().Descendants().Where(e => e.Name == "objectgroup");
			 foreach (var objGroup in objGroups) {
			 	string name = objGroup.Attribute("name").Value;
			 	foreach (var obj in objGroup.Descendants().Where(e => e.Name == "object")) {
			 		loadObject(obj,name);
			 	}	
			 }
		}
		
		void loadObject(XElement obj, string objectGroupName){
			int x = Int32.Parse(obj.Attribute("x").Value);
			int y = Int32.Parse(obj.Attribute("y").Value);
			var propertiesElemens = obj.Descendants().First(e => e.Name == "properties").Descendants();
			Dictionary<string, string> properties = new Dictionary<string, string>();
			
			foreach (var property in propertiesElemens) {
				string name = property.Attribute("name").Value;
				string value = property.Attribute("value").Value;
				properties.Add(name,value);
			}
			
			addObject(objectGroupName,x,y,properties);
		}
		
		protected abstract void addObject(string objectGroupName, int x, int y, Dictionary<string, string> properties);
		
		#region loadMapLayerRegion
		
		void loadLevelsLayers(){
			var layers = mapElement.Elements("layer");
			foreach (var layer in layers) {
				loadLayer(layer);
			}
		}
	
		void loadLayer(XElement layer){
			int width = Int32.Parse(layer.Attribute("width").Value);
			int height = Int32.Parse(layer.Attribute("height").Value);
			
			createNewLayer(layer, width, height);
			loadLayerTiles(layer, height);
		}
	
		void createNewLayer(XElement layer, int width, int height){
			Dictionary<string, string> properties = new Dictionary<string, string>();
			
			if(layer.Elements("properties").Any() ){
				var propertiesElements = layer.Element("properties").Descendants();
				foreach (var property in propertiesElements) {
					string pname = property.Attribute("name").Value;
					string value = property.Attribute("value").Value;
					properties.Add(pname, value);
				}
			}
			
			string name = layer.Attribute("name").Value;
			addLayer(name, width, height, properties);
		}
		
		protected abstract void addLayer(string layerName, int width, int height, Dictionary<string, string> properties);
	
		void loadLayerTiles(XElement layer, int height){
			XElement data = layer.Element("data");
			if(data.Attribute("encoding").Value == "csv"){
				string tilesCSV = data.Value;
				string[] tilesLines = tilesCSV.Split(new string[] { "\n\r", "\r\n", "\n", "\r" }, StringSplitOptions.None);
				int y = height;
				for (int i = 1; i <= height; i++) {
					y--;
					loadLayerLine( y, tilesLines[i]);
				}
			}else{
				outputError("The layer \"" + layer.Attribute("name").Value + "\" is not encoded in CSV, please change \"Map/Map Properties/Tile Layer Format\" to \"CSV\"");
			}
			
		}
		
		void loadLayerLine(int y, string tileLine){
			string[] tiles = tileLine.Split(new char[] { ',' }, StringSplitOptions.None);
			int x = 0;
			foreach (string tileId in tiles) {
				if(!tileId.Equals("0") && !tileId.Equals("") && tileId != null){
					int id = parseInt(tileId);
					if(callAddEmptyTiles && id == 0){
						addEmptyTiles(x,y);
					}else{
						addTile(x,y,id);
					}
					
				}
				x++;
			}
		}
	
		protected abstract void addTile(int x, int y, int id);
	
		protected virtual void addEmptyTiles(int x, int y) {
			
		}
		#endregion
		
		
		
		
		void loadMapProperties(){
			Dictionary<string, string> dictionnary = makePropertiesDictionary(mapElement.Element("properties"));
			if(dictionnary.Count > 0){
				loadMapProperty(dictionnary);
			}
		}
		
		protected abstract void loadMapProperty(Dictionary<string, string> properties);
		
	}
}