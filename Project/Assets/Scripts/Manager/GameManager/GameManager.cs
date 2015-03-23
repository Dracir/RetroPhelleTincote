using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class GameManager : StateLayer {
	
	public static GameManager instance;

	public string endGameSceneName;
	
	[Disable] public GameObject[] currentLevelPack;
	[Disable] public int currentLevelIndex = -1;
	
	[Disable] public GameObject levelGO;
	
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
		
		currentLevelIndex++;
		if(currentLevelIndex == currentLevelPack.Length){
			endGame();
		}else{
			loadLevel(currentLevelPack[currentLevelIndex]);
		}
	}

	
	void loadLevel(GameObject level) {
		SwitchState<GameManagerPause>();
		if(levelGO != null){
			Object.Destroy(levelGO);
		}
		
		levelGO = GameObjectExtend.createClone(level);
	}
	
	
	void endGame() {
		Application.LoadLevel(endGameSceneName);
	}
}
