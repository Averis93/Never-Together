using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InLevelManager : MonoBehaviour
{

	public Button NextLevel;
	public Text LevelNumber;
	
	// Use this for initialization
	void Start () {
		Button btnNextLevel = NextLevel.GetComponent<Button>();
		btnNextLevel.onClick.AddListener(StartNextLevel);
		StartCoroutine(FadeTextIn(0.7f));
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
	
	IEnumerator FadeTextIn(float t)
	{
		while (LevelNumber.color.a < 1.0f)
		{
			LevelNumber.color = new Color(LevelNumber.color.r, LevelNumber.color.g, LevelNumber.color.b, LevelNumber.color.a + (Time.deltaTime / t));
			yield return null;
		}
		yield return new WaitForSeconds(1.0f);
		StartCoroutine(FadeTextOut());
	}
	
	IEnumerator FadeTextOut()
	{
		while (LevelNumber.color.a > 0.0f)
		{
			LevelNumber.color = new Color(LevelNumber.color.r, LevelNumber.color.g, LevelNumber.color.b, LevelNumber.color.a - (Time.deltaTime));
			yield return null;
		}
	}
}
