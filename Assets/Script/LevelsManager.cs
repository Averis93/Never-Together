using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{

	[Header("Levels Btn")] 
	public Button[] Levels;
	
	void Start()
	{
		var btn1 = Levels[0].GetComponent<Button>();
		var btn2 = Levels[1].GetComponent<Button>();
		var btn3 = Levels[2].GetComponent<Button>();
		var btn4 = Levels[3].GetComponent<Button>();
		var btn5 = Levels[4].GetComponent<Button>();
		
		btn1.onClick.AddListener(StartLevel1);
		btn2.onClick.AddListener(StartLevel2);
		btn3.onClick.AddListener(StartLevel3);
		btn4.onClick.AddListener(StartLevel4);
		btn5.onClick.AddListener(StartLevel5);
	}

	void StartLevel1()
	{
		SceneManager.LoadScene("Level1");
	}
	
	void StartLevel2()
	{
		SceneManager.LoadScene("Level2");
	}
	
	void StartLevel3()
	{
		SceneManager.LoadScene("Level3");
	}
	
	void StartLevel4()
	{
		SceneManager.LoadScene("Level4");
	}
	
	void StartLevel5()
	{
		SceneManager.LoadScene("Level5");
	}
	
	// Get back to the levels menu
	public void BackToMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
