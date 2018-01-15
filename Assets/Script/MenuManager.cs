using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance { get; private set; }
	
	public Canvas Canvas;
    public int _countUseGame;

    public enum Menu
	{
		Main,
		Tutorial,
		Credits,
        General,
        Controls,
        PowerUp
	}
    
    [Header("Menu Screens")] 
	public GameObject MainMenu;
	public GameObject TutorialMenu;
	public GameObject CreditsMenu;
    public GameObject AudioPlay;
    public GameObject AudioMute;

    [Header("Tutorial Screens")]
    public GameObject GeneralMenu;
    public GameObject ControlsMenu;
    public GameObject PowerUpMenu;

    [Header("Controls Pages")]
    public GameObject SecondPageControls;

    [Header("General Pages")]
    public GameObject FirstPageGeneral;
    public GameObject SecondPageGeneral;
    public GameObject ThirdPageGeneral;

    [Header("Audio Mixer")]
    public AudioMixer mixer;
	public bool AudioDisabled;

    public bool _animOff;
    public Menu CurrentMenu { get; private set; }

    private bool _next;
    

    //void Awake()
    //{
    //    if (_audioMute)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    // Use this for initialization
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
    void Start ()
	{
        //PlayerPrefs.DeleteAll();
        Time.timeScale = 1f;
        _countUseGame = PlayerPrefs.GetInt("CountOpenGame");
        AudioDisabled = PlayerPrefs.GetInt("AudioMute") == 1 ? true : false;
        _animOff = false;
		CurrentMenu = Menu.Main;
        SwitchMenu(CurrentMenu);
	}

    void Update()
    {
        if (AudioDisabled)
        {
            mixer.SetFloat("Volume", -80f);
            AudioPlay.SetActive(false);
            AudioMute.SetActive(true);
        }

        if (!AudioDisabled)
        {
            mixer.SetFloat("Volume", 0f);
            AudioPlay.SetActive(true);
            AudioMute.SetActive(false);
        }
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

    public void ClickAudioMute()
    {
	    AudioDisabled = true;
        PlayerPrefs.SetInt("AudioMute", AudioDisabled ? 1 : 0);
    }

    public void ClickAudioPlay()
    {
	    AudioDisabled = false;
        PlayerPrefs.SetInt("AudioMute", AudioDisabled ? 1 : 0);
    }

    public void OnClickPlay()
	{
        //SoundManager.Instance.GameplaySoundtrack();
        GetComponent<AnimationControl>().enabled = false;
        _animOff = true;
        StartCoroutine(LoadLevelsAsync());
	}
	
	IEnumerator LoadLevelsAsync()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Levels");

		//Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		
		Canvas.gameObject.SetActive(false);
		
		if (LevelsManager.Instance != null)
		{
			LevelsManager.Instance.Canvas.gameObject.SetActive(true);
		}
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
        Canvas.gameObject.SetActive(true);
	}

    public void OnClickGeneral()
    {
        SwitchMenu(Menu.General);
        FirstPageGeneral.SetActive(true);
        SecondPageGeneral.SetActive(false);
        ThirdPageGeneral.SetActive(false);
    }

    // Get to the next page
    public void NextToSecondPageGeneral()
    {
        FirstPageGeneral.SetActive(false);
        SecondPageGeneral.SetActive(true);
    }

    // Back to the previous page
    public void BackToSecondPageGeneral()
    {
        SecondPageGeneral.SetActive(true);
        ThirdPageGeneral.SetActive(false);
    }

    // Get to the next page
    public void NextToThirdPageGeneral()
    {
        SecondPageGeneral.SetActive(false);
        ThirdPageGeneral.SetActive(true);
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
