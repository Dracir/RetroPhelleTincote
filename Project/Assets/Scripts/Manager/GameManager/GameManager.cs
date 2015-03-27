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
	
	public string endGameSceneName = "MainMenu";
	
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
	
	public void switchToLevelPack(string levelPackAssetFolder, int startingLevel = 0) {
		if(Application.loadedLevelName != "InGame"){
			LoadGame("InGame");
			Debug.Log("BOB");
		}
		
		currentLevelPack = Resources.LoadAll<GameObject>(levelPackAssetFolder);
		currentLevelIndex = startingLevel - 1;
		nextLevel();
	}

	IEnumerator LoadGame(string strLevel){
		Debug.Log("Loading Level");
		yield return Application.LoadLevelAsync(strLevel);
		Debug.Log("Level Load complete");
	}
	
	public void nextLevel(){
		if(currentLevelPack.Length == 0) return;
		
		if(player1 == null){
			setUpPlayers();
		}
		
		currentLevelIndex++;
		
		PlayerPrefs.SetInt("MAX_LEVEL",currentLevelIndex);
		
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
		SwitchState<GameManagerInGame>();
		if(levelGO != null){
			Object.Destroy(levelGO);
		}
		
		levelGO = GameObjectExtend.createClone(level);
		mapData = levelGO.GetComponent<MapData>();
		
		relocatePlayersToStart();
		centerCamera();
	}

	public void relocatePlayersToStart() {
		GameObject p1Start = levelGO.FindChild("Player1Start");
		if(p1Start && player1){
			player1.transform.position = p1Start.transform.position;
		}
		
		GameObject p2Start = levelGO.FindChild("Player2Start");
		if(p2Start && player2){
			player2.transform.position = p2Start.transform.position;
		}
	}
	
	public void relocatePlayerToStart (GameObject player){
		int number = 0;
		if (player == player1){
			number = 1;
		} else if (player == player2){
			number = 2;
		} else {
			Debug.Log("Trying to respawn something that isn't a player :S ");
			return;
		}
		GameObject startObj = levelGO.FindChild("Player" + number.ToString() + "Start");
		if (player && startObj){
			player.transform.position = startObj.transform.position;
		}
	}

	void centerCamera() {
		Camera.main.gameObject.transform.position = new Vector3(mapData.width / 2 - 0.5f, mapData.height/2 -0.5f , -10);
		Camera.main.orthographicSize = mapData.height/2;
	}
	
	
	void endGame() {
		Application.LoadLevel(endGameSceneName);
	}
}
