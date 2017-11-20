using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InLevelManager : MonoBehaviour
{

	public Button NextLevel;
	
	// Use this for initialization
	void Start () {
		Button btnNextLevel = NextLevel.GetComponent<Button>();
		btnNextLevel.onClick.AddListener(StartNextLevel);
	}

	void StartNextLevel()
	{
		string sceneName = SceneManager.GetActiveScene().name;
		string levelNumber = sceneName.Substring(sceneName.Length - 1);

		switch (levelNumber)
		{
			case "1":
				SceneManager.LoadScene("Level2");
				break;
			case "2":
				SceneManager.LoadScene("Level3");;
				break;
			case "3":
				SceneManager.LoadScene("Level4"); 
				break;
			case "4":
				SceneManager.LoadScene("Level5");
				break;
		}
	}
}
