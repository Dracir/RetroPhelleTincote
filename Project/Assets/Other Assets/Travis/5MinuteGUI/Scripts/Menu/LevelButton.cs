using UnityEngine;
using System.Collections;
namespace FMG
{
	public class LevelButton : MonoBehaviour {
		public int levelIndex=0;

		public void onClick()
		{
			GameManager.instance.switchToLevelPack("Maps/Game", levelIndex - 1);
		}
	}
}