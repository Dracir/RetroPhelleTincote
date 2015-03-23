using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Rick.TiledMapLoader{
	public abstract class TiledLoader {
	
		protected Dictionary<string, string> makePropertiesDictionary(XElement propertiesElement){
			Dictionary<string, string> properties = new Dictionary<string, string>();
			if(propertiesElement == null) return properties;
			
			foreach (var property in propertiesElement.Elements("property")) {
				string name = property.Attribute("name").Value;
				string value = property.Attribute("value").Value;
				properties.Add(name,value);
			}
			
			return properties;
		}
		
		protected int parseInt(string intStr){
			try{
				int id = Int32.Parse(intStr);
				return id;
			}catch (OverflowException){
				UnityEngine.Debug.LogError(intStr + " overflow the memory :(");
			}
			return -1;
		}
		
		protected void debugLog(string log){
			UnityEngine.Debug.Log(log);
		}
		
		
		protected void outputError(string message){
			UnityEngine.Debug.LogError(message);
		
		}
	}
}