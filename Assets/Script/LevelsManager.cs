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
		Button btn1 = Levels[0].GetComponent<Button>();
		Button btn2 = Levels[1].GetComponent<Button>();
		Button btn3 = Levels[2].GetComponent<Button>();
		Button btn4 = Levels[3].GetComponent<Button>();
		Button btn5 = Levels[4].GetComponent<Button>();
		
		btn1.onClick.AddListener(StartLevel1);
		btn3.onClick.AddListener(StartLevel3);
	}

	void StartLevel1()
	{
		SceneManager.LoadScene("Level1");
	}
	
	void StartLevel3()
	{
		SceneManager.LoadScene("Level3");
	}
	
}
