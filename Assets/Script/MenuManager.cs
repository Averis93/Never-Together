using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public enum Menu
	{
		Main,
		Tutorial,
		Credits,
        General,
        Controls,
        PowerUp,
        Second
	}

	[Header("Menu Screens")] 
	public GameObject MainMenu;
	public GameObject TutorialMenu;
	public GameObject CreditsMenu;

    [Header("Tutorial Screens")]
    public GameObject GeneralMenu;
    public GameObject ControlsMenu;
    public GameObject PowerUpMenu;

    [Header("Controls Pages")]
    public GameObject SecondPageControls;

    [Header("General Pages")]
    public GameObject FirstPageGeneral;
    public GameObject SecondPageGeneral;

    public Menu CurrentMenu { get; private set; }

    private bool _next;
	
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
			case Menu.Tutorial:
				TutorialMenu.SetActive(true);
				break;
			case Menu.Credits:
				CreditsMenu.SetActive(true);
				break;
            case Menu.General:
                GeneralMenu.SetActive(true);
                break;
            case Menu.Controls:
                ControlsMenu.SetActive(true);
                break;
            case Menu.PowerUp:
                PowerUpMenu.SetActive(true);
                break;
        } 
	}

	void DisableMenus()
	{
		MainMenu.SetActive(false);	
		TutorialMenu.SetActive(false);	
		CreditsMenu.SetActive(false);
        GeneralMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        PowerUpMenu.SetActive(false);
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
	
	public void OnClickTutorial()
	{
		SwitchMenu(Menu.Tutorial);
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

    public void OnClickGeneral()
    {
        SwitchMenu(Menu.General);
        FirstPageGeneral.SetActive(true);
        SecondPageGeneral.SetActive(false);
    }

    // Get to the next page
    public void NextToSecondPageGeneral()
    {
        FirstPageGeneral.SetActive(false);
        SecondPageGeneral.SetActive(true);
    }

    public void OnClickControls()
    {
        SwitchMenu(Menu.Controls);
        SecondPageControls.SetActive(false);
    }

    // Get to the next page
    public void NextToSecondPageControls()
    {
        SecondPageControls.SetActive(true);
    }

    public void OnClickPowerUp()
    {
        SwitchMenu(Menu.PowerUp);
    }

    // Get back to the levels menu
    public void BackToTutorial()
    {
        SwitchMenu(Menu.Tutorial);
    }
}
