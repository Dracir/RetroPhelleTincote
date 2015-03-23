using UnityEngine;
using System.Collections;

public class BuyPassMenu : MonoBehaviour {

	public string levelPackAssetFolder = "Maps/Tests";
	public string endGameSceneName = "Rick";
	
	void Start () {
		GameManager.instance.endGameSceneName = endGameSceneName;
		GameManager.instance.switchToLevelPack(levelPackAssetFolder);
		Object.Destroy(gameObject);
	}
}
