using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InLevelManager : MonoBehaviour
{
	//public Button NextLevel;
	public Text LevelNumber;

	public Transform AdditionalTime;
	public Canvas Canvas;
	public Camera Cam;

	public GameObject[] Magnets;
	public GameObject Background;
    public GameObject InfiniteBackground;
    public GameObject Bots;
	
	// Coins count string
	public Text CoinsCountText;
	
	// Lives
	public GameObject[] Lives;
	public GameObject AdditionalLife;
	
	// Timer
	public Text Timer;
	public int TimeAfterCollision;
	
	// Particle effect
	[Header("Effects")]
	public GameObject[] ParticleEffect;
    public GameObject[] AttractionEffect;
    public GameObject[] ShieldEffect;
	public float CamShakeAmt = 0.1f;

    // Handle power-ups
    [Header("Power-Ups")]
	public int PowerUpDuration = 20;
	public GameObject[] AttractionPowerUp;
	public GameObject[] SlowdownPowerUp;
	
	// Ending of the level
	[Header("End level")]
	public GameObject GameOver;
	public GameObject Statistics;
	public Text TotalCoinsCollected;
	public Text TotalLivesLost;
	public Text TimerText;
	public GameObject TotalCoins;
	public GameObject[] Stars;
	
	
	private int _coinsCount;
	private int _maxCoins;
	private int _totalCoinsCollected;
	private int _totalCoins;
	private int _remainingLives;
	private int _livesLost;
	private bool _additionalLife;
	private bool _gameover;
	private bool _stopGame;
	private CameraShake _camShake;

	private Text _attractionTimer;
	private Text _slowdownTimer;
	private float _slowdownSpeed;
	
	// Use this for initialization
	void Start () {
		_coinsCount = 0;
		_maxCoins = 150;
		_totalCoinsCollected = 0;
		_totalCoins = TotalCoins.transform.childCount;
		SetCountText();
		_remainingLives = 3;
		_livesLost = 0;
		_additionalLife = false;
		_gameover = false;
		TimeAfterCollision = 0;
		_stopGame = false;
		_camShake = GetComponent<CameraShake>();

		_attractionTimer = AttractionPowerUp[0].transform.GetChild(0).gameObject.GetComponent<Text>();
		_slowdownTimer = SlowdownPowerUp[0].transform.GetChild(0).gameObject.GetComponent<Text>();
		_slowdownSpeed = 0.7f;
		
		StartCoroutine(StartTimer());
		StartCoroutine(FadeTextIn(0.7f));
	}
	
	void LateUpdate()
	{
		// If the two magnets collide, remove 1 heart
		if (Magnets[0].GetComponent<CharacterBehaviour>().JumpedUp &&
		    Magnets[1].GetComponent<CharacterBehaviour>().JumpedUp)
		{	
			// Disable keyboard input
			Magnets[0].GetComponent<CharacterBehaviour>().SetInput(false);
			Magnets[1].GetComponent<CharacterBehaviour>().SetInput(false);
			
			// Create sparks
			Invoke("ImpactEffect", 0.4f);
			
			//Remove a life, add time and gameover in case lives = 0
			RemoveLife();
			SetTimeAfterCollision(Cam.WorldToScreenPoint(new Vector3(0.0f, 0.0f, 0.0f)));

			if (!_gameover)
			{
				Magnets[0].GetComponent<CharacterBehaviour>().JumpedUp = false;
				Magnets[1].GetComponent<CharacterBehaviour>().JumpedUp = false;

				Invoke("BringMagnetsDown", 1.5f);
			}
		}
	}

	// Start next level when the player taps the right arrow at the end of a level
	public void StartNextLevel()
	{
		var sceneName = SceneManager.GetActiveScene().name;
		var levelNumber = sceneName.Substring(sceneName.Length - 1);

		switch (levelNumber)
		{
			case "1":
				SceneManager.LoadScene("Level2");
				break;
			case "2":
				SceneManager.LoadScene("Level3");
				break;
			case "3":
				SceneManager.LoadScene("Level4"); 
				break;
			case "4":
				SceneManager.LoadScene("Level5");
				break;
			default: 
				Debug.Log("Couldn't find level");
				break;
		}
	}

	// Get back to the levels menu
	public void BackToLevels()
	{
		SceneManager.LoadScene("Levels");
	}
	
	// Fade in the text 'Level n' at the beginning of a level
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
	
	// Fade out the text 'Level n' at the beginning of a level
	IEnumerator FadeTextOut()
	{
		while (LevelNumber.color.a > 0.0f)
		{
			LevelNumber.color = new Color(LevelNumber.color.r, LevelNumber.color.g, LevelNumber.color.b, LevelNumber.color.a - (Time.deltaTime));
			yield return null;
		}
	}
	
	// Sets the text string specifying the number of collected coins
	public void SetCountText()
	{
		CoinsCountText.text = _coinsCount.ToString();
	}
	
	// Sets the number of collected coins
	public void SetCount()
	{
		_coinsCount++;
		_totalCoinsCollected++;
		
		// If the player collects 200 coins, add a life and reset the coins count
		if (_coinsCount == _maxCoins)
		{
			_coinsCount = 0;

			if (_remainingLives == 3)
			{
				if (!_additionalLife)
				{
					AddLife(AdditionalLife);
					_additionalLife = true;
				}
			}
			else
			{
				AddLife(Lives[_remainingLives]);
				_remainingLives = _remainingLives + 1;
			}
			
			SetCountText();
		}
	}
	
	// When the 2 magnets collide or when they hit a tree remove a life. If no more lives are available, gameover.
	public void RemoveLife()
	{
		if (_additionalLife)
		{
			AdditionalLife.SetActive(false);
			_additionalLife = false;
		}
		else
		{
			_remainingLives = _remainingLives - 1;
			_livesLost++;
			Lives[_remainingLives].SetActive(false);
			
			if (_remainingLives == 0)
			{
				_gameover = true;
				GameOver.SetActive(true);
			
				// END THE GAME!!!
			}
		}
	}
	
	// Add a red heart when the player collects 200 coins
	void AddLife(GameObject life)
	{
		life.SetActive(true);
		life.GetComponent<Life>().ChangeShape();
		
	}
	
	// Timer for the level
	IEnumerator StartTimer()
	{
		var seconds = 0;
		var minutes = 0;

		Timer.text = "0.00";
		
		while (!_stopGame)
		{
			while (TimeAfterCollision != 0)
			{
				yield return new WaitForSeconds(0.05f);
				seconds ++;
				TimeAfterCollision--;
				
				if (seconds < 10)
				{
					Timer.text = minutes + ".0" + seconds;
				}
				else if (seconds == 60)
				{
					minutes++;
					seconds = 0;

					Timer.text = minutes + ".0" + seconds;
				}
				else
				{
					Timer.text = minutes + "." + seconds;
				}
			}
			
			yield return new WaitForSeconds(1.0f);
			seconds++;
			
			if (seconds < 10)
			{
				Timer.text = minutes + ".0" + seconds;
			}
			else if (seconds == 60)
			{
				minutes++;
				seconds = 0;

				Timer.text = minutes + ".0" + seconds;
			}
			else
			{
				Timer.text = minutes + "." + seconds;
			}
		}	
		
		TimerText.transform.GetComponent<Text>().text = Timer.text;
		TotalCoinsCollected.text = _totalCoinsCollected + "/" + _totalCoins;
		TotalLivesLost.text = _livesLost.ToString();
		Statistics.SetActive(true);
		StartCoroutine(CheckStars());

		
		//var levels = SceneManager.GetSceneByName("DontDestroyOnLoad").GetRootGameObjects()[0].GetComponent<LevelsManager>();
		var currentScene = SceneManager.GetActiveScene().name;
		LevelsManager.Control.Locked[Int32.Parse(currentScene.Substring(currentScene.Length-1))+1] = false;
	}

	IEnumerator CheckStars()
	{
		var activeStars = new List<GameObject>();
		
		if (_totalCoinsCollected < _totalCoins) // Valore da cambiare
		{
			activeStars.Add(Stars[0].transform.GetChild(1).gameObject);
		}
		else
		{
			activeStars.Add(Stars[0].transform.GetChild(0).gameObject);
		}

		if (_livesLost != 0) // Valore da cambiare
		{
			activeStars.Add(Stars[1].transform.GetChild(1).gameObject);
		}
		else
		{
			activeStars.Add(Stars[1].transform.GetChild(0).gameObject);
		}

		var time = 0.0f;
		float.TryParse(Timer.text, out time);
		if (time > 1.22f) // Valore da cambiare
		{
			activeStars.Add(Stars[2].transform.GetChild(1).gameObject);
		}
		else
		{
			activeStars.Add(Stars[2].transform.GetChild(0).gameObject);
		}

		var i = 0;
		while (i < activeStars.Count)
		{
			activeStars[i].SetActive(true);
			activeStars[i].GetComponent<Star>().ChangeShape();
			i++;
			yield return new WaitForSeconds(0.5f);
		}
	}

	// Add 20 seconds to the total time after every collision with a branch or a bot
	public void SetTimeAfterCollision(Vector3 position)
	{
		var testo = AdditionalTime.GetComponent<Text>();
		var addTime = Instantiate(testo, position, Quaternion.identity);
		addTime.transform.SetParent(Canvas.transform, false);
	}

	// Displays the statistics on the screen at the end of a level
	public void ShowStatistics()
	{
		_stopGame = true;
		
		// Disable keyboard input
		Magnets[0].GetComponent<CharacterBehaviour>().SetInput(false);
		Magnets[1].GetComponent<CharacterBehaviour>().SetInput(false);
	}
	
	void EnableKeyboardInput()
	{
		Magnets[0].GetComponent<CharacterBehaviour>().SetInput(true);
		Magnets[1].GetComponent<CharacterBehaviour>().SetInput(true);
	}
	
	// Enable attraction power-up
	public void Attraction()
	{
		AttractionPowerUp[0].SetActive(true);
		AttractionPowerUp[1].SetActive(true);
		AttractionPowerUp[2].SetActive(true);

        AttractionEffect[0].SetActive(true);
        AttractionEffect[1].SetActive(true);
        StartCoroutine(StartCountdown(PowerUpDuration, AttractionPowerUp, _attractionTimer, "Attraction"));
	}
	
	// Enable slowdown power-up
	public void Slowdown()
	{
		SlowdownPowerUp[0].SetActive(true);
		Background.GetComponent<BackgroundMove>().Speed *= _slowdownSpeed;
        InfiniteBackground.GetComponent<BackgroundMove>().Speed *= _slowdownSpeed;

        for (var i = 0; i < Bots.transform.childCount; i++)
		{
			Bots.transform.GetChild(i).GetComponent<BotController>().Speed *= _slowdownSpeed;
		}
		
		StartCoroutine(StartCountdown(PowerUpDuration, SlowdownPowerUp, _slowdownTimer, "Slowdown"));
	}
	    
	// Timer for power-ups
	IEnumerator StartCountdown(int countdownValue, GameObject[] powerUp, Text powerUpTimer, string type)
	{
		powerUpTimer.text = ":" + countdownValue;
        
		while (countdownValue > 0)
		{
			yield return new WaitForSeconds(1.0f);
			countdownValue--;
			powerUpTimer.text = ":" + countdownValue;
		}
		
		if (type == "Slowdown")
		{
			Background.GetComponent<BackgroundMove>().Speed /= _slowdownSpeed;
            InfiniteBackground.GetComponent<BackgroundMove>().Speed /= _slowdownSpeed;

            for (var i = 0; i < Bots.transform.childCount; i++)
			{
				Bots.transform.GetChild(i).GetComponent<BotController>().Speed /= _slowdownSpeed;
			}
		}

		for (var i = 0; i < powerUp.Length; i++)
		{
			powerUp[i].SetActive(false);
		}

        AttractionEffect[0].SetActive(false);
        AttractionEffect[1].SetActive(false);
    }
		
	void ImpactEffect()
	{
		ParticleEffect[0].SetActive(true);
		ParticleEffect[1].SetActive(true);

		//Shake the camera
		_camShake.Shake(CamShakeAmt, 0.1f);
	}

	// Move the magnets back on the surface when they collide
	void BringMagnetsDown()
	{
		ParticleEffect[0].SetActive(false);
		ParticleEffect[1].SetActive(false);
		Magnets[0].GetComponent<CharacterBehaviour>().ChangePos = false;
		Magnets[1].GetComponent<CharacterBehaviour>().ChangePos = false;
		
		// Enable keyboard input
		Invoke("EnableKeyboardInput", 0.8f);
	}
}
