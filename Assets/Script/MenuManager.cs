using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public enum Menu
	{
		Main,
		Settings,
		Credits
	}

	[Header("Menu Screens")] 
	public GameObject MainMenu;
	public GameObject SettingsMenu;
	public GameObject CreditsMenu;
	
	public Menu CurrentMenu { get; private set; }
	
	// Use this for initialization
	void Start ()
	{
		CurrentMenu = Menu.Main;
		SwitchMenu(CurrentMenu);
	}

	void SwitchMenu(Menu menu)
	{
		DisableMenus();

		switch (menu)
		{
			case Menu.Main:
				MainMenu.SetActive(true);
				break;
			case Menu.Settings:
				SettingsMenu.SetActive(true);
				break;
			case Menu.Credits:
				CreditsMenu.SetActive(true);
				break;
		} 
	}

	void DisableMenus()
	{
		MainMenu.SetActive(false);	
		SettingsMenu.SetActive(false);	
		CreditsMenu.SetActive(false);	
	}
/*
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P))
		{
			//SoundManager.Instance.GameplaySoundtrack();
			SceneManager.LoadScene("Levels");
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwitchMenu(Menu.Main);
		} 
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwitchMenu(Menu.Settings);
		} 
		
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwitchMenu(Menu.Credits);
		} 
	}
*/
	public void OnClickPlay()
	{
		//SoundManager.Instance.GameplaySoundtrack();
		SceneManager.LoadScene("Levels");
	}
	
	public void OnClickSettings()
	{
		SwitchMenu(Menu.Settings);
	}
	
	public void OnClickCredits()
	{
		SwitchMenu(Menu.Credits);
	}
	
	
	// Get back to the levels menu
	public void BackToMenu()
	{
		SwitchMenu(Menu.Main);
	}
}
