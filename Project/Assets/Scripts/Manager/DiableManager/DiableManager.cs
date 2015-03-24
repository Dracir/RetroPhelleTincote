using UnityEngine;
using System.Collections.Generic;

public class DiableManager : MonoBehaviour {

	public List<Diable> diables = new List<Diable>();
	public List<Diable> diablesToAdd = new List<Diable>();
	public List<Diable> diablesToRemove = new List<Diable>();
		
	public static DiableManager instance;
	
	void Awake () {
		DiableManager.instance = this;
	}
	
	
	void Update () {
		foreach (var diable in diables) {
			if( !diable.isAlive ){
				diablesToRemove.Add(diable);
			}
		}
		
		foreach (var toRemove in diablesToRemove) {
			diables.Remove(toRemove);
			Object.Destroy(toRemove.gameObject);
		}
		diablesToRemove.Clear();
		
		foreach (var toAdd in diablesToAdd) {
			diables.Add(toAdd);
		}
		diablesToAdd.Clear();
	}
	
	public void addDiable(Diable diable){
		diablesToAdd.Add(diable);
		diable.transform.parent = this.transform;
	}
}
