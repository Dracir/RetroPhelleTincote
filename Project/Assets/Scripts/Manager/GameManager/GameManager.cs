using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameManager : StateLayer {
	
	public static GameManager instance;

	public GameObject player1prefab;
	public GameObject player2prefab;
	
	[Disable] public GameObject player1;
	[Disable] public GameObject player2;
	
	public string endGameSceneName;
	
	[Disable] public GameObject[] currentLevelPack;
	[Disable] public int currentLevelIndex = -1;
	
	[Disable] public GameObject levelGO;
	[Disable] public MapData mapData;
	
	void Awake(){
		if(instance != null && instance != this){
			Object.Destroy(gameObject);
		}else{
			instance = this;
			Object.DontDestroyOnLoad(gameObject);
		}
	}
	
	public void switchToLevelPack(string levelPackAssetFolder) {
		currentLevelPack = Resources.LoadAll<GameObject>(levelPackAssetFolder);
		currentLevelIndex = -1;
		nextLevel();
	}
	
	
	public void nextLevel(){
		if(currentLevelPack.Length == 0) return;
		
		if(player1 == null){
			setUpPlayers();
		}
		
		currentLevelIndex++;
		if(currentLevelIndex == currentLevelPack.Length){
			endGame();
		}else{
			loadLevel(currentLevelPack[currentLevelIndex]);
		}
	}

	void setUpPlayers() {
		if(player1prefab == null){
			Debug.LogError("Il n'y a pas de prefab pour le player 1 !!!");
		}else{
			player1 = GameObjectExtend.createClone(player1prefab);
		}
		
		if(player2prefab != null){
			player2 = GameObjectExtend.createClone(player2prefab);
		}
	}
	
	void loadLevel(GameObject level) {
		SwitchState<GameManagerPause>();
		if(levelGO != null){
			Object.Destroy(levelGO);
		}
		
		levelGO = GameObjectExtend.createClone(level);
		mapData = levelGO.GetComponent<MapData>();
		
		relocatePlayersToStart();
		centerCamera();
	}

	void relocatePlayersToStart() {
		GameObject p1Start = levelGO.FindChild("Player1Start");
		if(p1Start && player1){
			player1.transform.position = p1Start.transform.position;
		}
		
		GameObject p2Start = levelGO.FindChild("Player2Start");
		if(p2Start && player2){
			player2.transform.position = p2Start.transform.position;
		}
	}

	void centerCamera() {
		Camera.main.gameObject.transform.position = new Vector3(mapData.width / 2, mapData.height/2, -10);
		Camera.main.orthographicSize = mapData.height/2;
	}
	
	
	void endGame() {
		Application.LoadLevel(endGameSceneName);
	}
}
