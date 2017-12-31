﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{

	[Header("Levels Btn")] 
	public Button[] Levels;

	public bool[] Locked;

	public static LevelsManager Control;

	void Awake()
	{
		Control = this;                          // linking the self-reference
		DontDestroyOnLoad(transform.gameObject); // set to dont destroy
	}
	
	void Start()
	{
		
		//Locked = new[] {false, true, true, true, true, true};
		
		var btn1 = Levels[0].GetComponent<Button>();
		var btn2 = Levels[1].GetComponent<Button>();
		var btn3 = Levels[2].GetComponent<Button>();
		var btn4 = Levels[3].GetComponent<Button>();
		var btn5 = Levels[4].GetComponent<Button>();
        var btn6 = Levels[5].GetComponent<Button>();
        var btn7 = Levels[6].GetComponent<Button>();
        var btn8 = Levels[7].GetComponent<Button>();

        btn1.onClick.AddListener(StartLevel1);
		btn2.onClick.AddListener(StartLevel2);
		btn3.onClick.AddListener(StartLevel3);
		btn4.onClick.AddListener(StartLevel4);
		btn5.onClick.AddListener(StartLevel5);
        btn6.onClick.AddListener(StartLevel6);
        btn7.onClick.AddListener(StartLevel7);
        btn8.onClick.AddListener(StartLevel8);
    }

	void StartLevel1()
	{
		//Debug.Log("Level1: " + Locked[0]);
		SceneManager.LoadScene("Level1");	
	}
	
	void StartLevel2()
	{
		/*
		Debug.Log("Level2: " + Locked[1]);
		if (!Locked[1])
		{
			SceneManager.LoadScene("Level2");
		}
		*/
		
		SceneManager.LoadScene("Level2");
	}
	
	void StartLevel3()
	{
		
		/*
		Debug.Log("Level3: " + Locked[2]);
		if (!Locked[2])
		{
			SceneManager.LoadScene("Level3");
		}
		*/
		
		SceneManager.LoadScene("Level3");
	}
	
	void StartLevel4()
	{
		/*
		if (!Locked[3])
		{
			SceneManager.LoadScene("Level4");
		}
		*/
		
		SceneManager.LoadScene("Level4");
	}
	
	void StartLevel5()
	{
		/*
		if (!Locked[4])
		{
			SceneManager.LoadScene("Level5");
		}
		*/
		
		SceneManager.LoadScene("Level5");
	}

    void StartLevel6()
    {
        /*
		if (!Locked[5])
		{
			SceneManager.LoadScene("Level6");
		}
		*/

        SceneManager.LoadScene("Level6");
    }

    void StartLevel7()
    {
        /*
		if (!Locked[6])
		{
			SceneManager.LoadScene("Level7");
		}
		*/

        SceneManager.LoadScene("Level7");
    }

    void StartLevel8()
    {
        /*
		if (!Locked[7])
		{
			SceneManager.LoadScene("BonusLevel");
		}
		*/

        SceneManager.LoadScene("BonusLevel");
    }

    // Get back to the levels menu
    public void BackToMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
