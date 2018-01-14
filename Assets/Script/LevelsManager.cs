using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class LevelsManager : MonoBehaviour
{
	public static LevelsManager Instance { get; private set; }

    [Header("StarsNum")] 
	public Text StarsText;
	
	[Header("Levels Btn")] 
	public Button[] Levels;    

	public GameObject[] LevelStars;
	public int[] StarsForLevel;

	public bool[] Locked;
    private bool[] LockedArray;

	public Canvas Canvas;
    private int _starsCollected;    

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
	
	void Start()
	{
		Time.timeScale = 1f;

        if (MenuManager.Instance._countUseGame == 0)
        {
            Locked = new[] { false, true, true, true, true, true, true, true };
            StarsForLevel = new[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        }
        else if (MenuManager.Instance._countUseGame == 1)
        {
            Locked = PlayerPrefsX.GetBoolArray("LockedLevel");
            StarsForLevel = PlayerPrefsX.GetIntArray("StarsForLevels");
        }        

        LevelStars = new GameObject[3];

        _starsCollected = 0;
        _starsCollected = PlayerPrefs.GetInt("StarsCollected");

        StarsText.text = "0";
        StarsText.text = _starsCollected.ToString();

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
        btn8.onClick.AddListener(StartBonusLevel);
    }

    void Update()
    {
        //per togliere l'immagine di blocco ai livelli ormai sbloccati e visualizzare stelle una volta riavviato gioco
        for (int i = 0; i < Locked.Length; i++)
        {
            if (!Locked[i])
            {
                if (StarsForLevel[i] > 0)
                {
                    var StarsObtained = StarsForLevel[i];
                    switch (StarsObtained) {

                        case 1:
                            Levels[i].transform.Find("Stars_2").gameObject.SetActive(true);
                            break;
                        case 2:
                            Levels[i].transform.Find("Stars_3").gameObject.SetActive(true);
                            break;
                        case 3:
                            Levels[i].transform.Find("Stars_4").gameObject.SetActive(true);
                            break;
                    }
                }
            }

            if (!Locked[i] && i != 0)
            {
                Levels[i].transform.Find("Locked").gameObject.SetActive(false);
            }            
        }

    }

    void StartLevel1()
	{
        StartCoroutine(Load("Level1"));
        //SceneManager.LoadScene("Level1");
        AssignStars(0);
	}
	
	public void StartLevel2()
	{
		if (!Locked[1])
		{
            StartCoroutine(Load("Level2"));
            //SceneManager.LoadScene("Level2");
		}
		
		AssignStars(1);
	
		
		//SceneManager.LoadScene("Level2");
	}
	
	public void StartLevel3()
	{
		if (!Locked[2])
		{
            StartCoroutine(Load("Level3"));
            //SceneManager.LoadScene("Level3");
        }
		
		AssignStars(2);
		
		
		//SceneManager.LoadScene("Level3");
	}
	
	public void StartLevel4()
	{
		if (!Locked[3])
		{
            StartCoroutine(Load("Level4"));
            //SceneManager.LoadScene("Level4");
		}
        
        AssignStars(3);
		
		
		//SceneManager.LoadScene("Level4");
	}
	
	public void StartLevel5()
	{
		if (!Locked[4])
		{
            StartCoroutine(Load("Level5"));
            //SceneManager.LoadScene("Level5");
		}
		
		AssignStars(4);
		
		
		//SceneManager.LoadScene("Level5");
	}

	public void StartLevel6()
    {
		if (!Locked[5])
		{
            StartCoroutine(Load("Level6"));
            //SceneManager.LoadScene("Level6");
        }
		
	    AssignStars(5);
	    

        //SceneManager.LoadScene("Level6");
    }

	public void StartLevel7()
    {
        
		if (!Locked[6])
		{
            StartCoroutine(Load("Level7"));
            //SceneManager.LoadScene("Level7");
		}
	    
	    AssignStars(6);
		

        //SceneManager.LoadScene("Level7");
    }

	public void StartBonusLevel()
    {
        
		if (!Locked[7])
		{
            StartCoroutine(Load("BonusLevel"));
            //SceneManager.LoadScene("BonusLevel");
        }

	    AssignStars(7);


	    //SceneManager.LoadScene("BonusLevel");
    }

    // Get back to the levels menu
    public void BackToMenu()
	{
		StartCoroutine(LoadMenuAsync());
		//SceneManager.LoadScene("Menu");
	}

	IEnumerator LoadMenuAsync()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");

		//Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		
		MenuManager.Instance.Canvas.gameObject.SetActive(true);
        MenuManager.Instance.GetComponent<AnimationControl>().enabled = true;
        
        Canvas.gameObject.SetActive(false);
	}

    IEnumerator Load(String nameLevel)
    {
        #if UNITY_IPHONE
            Handheld.SetActivityIndicatorStyle(iOS.ActivityIndicatorStyle.Gray);
        #elif UNITY_ANDROID
            Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
        #endif

        Handheld.StartActivityIndicator();
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(nameLevel);
    }

    void AssignStars(int index)
	{
		LevelStars[0] = Levels[index].transform.Find("Stars_2").gameObject;
		LevelStars[1] = Levels[index].transform.Find("Stars_3").gameObject;
		LevelStars[2] = Levels[index].transform.Find("Stars_4").gameObject;
	}

	public void UnlockNewLevel(int index)
	{
		Locked[index] = false;
        for(int i = 0; i == index; i++)
        {
            PlayerPrefsX.SetBoolArray("LockedLevel", Locked);
        }
        for (int i = 0; i < Locked.Length ; i++)
        {
            PlayerPrefsX.SetBoolArray("LockedLevel", Locked);
        }

        Levels[index].transform.Find("Locked").gameObject.SetActive(false);
    }

	// Returns true if the arrow to start the next level has to be canceled
	public bool SetStars(int starsNum, int level)
	{
        MenuManager.Instance._countUseGame = 1;
        PlayerPrefs.SetInt("CountOpenGame", MenuManager.Instance._countUseGame);

		if (starsNum > StarsForLevel[level - 1])
		{
			_starsCollected = _starsCollected - StarsForLevel[level - 1] + starsNum;
            PlayerPrefs.SetInt("StarsCollected", _starsCollected);

            StarsText.text = "" + _starsCollected; 
			StarsForLevel[level - 1] = starsNum;

            for (int i = 0; i < StarsForLevel.Length; i++)
            {
                PlayerPrefsX.SetIntArray("StarsForLevels", StarsForLevel);
            }

            LevelStars[starsNum - 1].SetActive(true);
			
			if (_starsCollected == 21)
			{
				UnlockNewLevel(7);
				return false;
			}
			
			if (level == 7)
			{
				return true;
			}
		}

		return false;
	}
}
