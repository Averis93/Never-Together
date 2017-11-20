using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class MagnetsController : MonoBehaviour {
	
	// Coins count string
	public Text CoinsCountText;
	
	// Lives
	public GameObject[] Lives;
	public GameObject AdditionalLife;

	// Ending of the game
	public GameObject GameOver;
	public GameObject Statistics;
	public Text TotalCoinsCollected;
	public Text TotalLivesLost;
	public Text TimerText;
	public GameObject TotalCoins;

    // Particle effect
    public GameObject[] ParticleEffect;

    // Handle Camera shake
    public float CamShakeAmt = 0.1f;
    public GameObject AppManager;
	
	// Handle power-ups
	public int PowerUpDuration = 20;
	public GameObject PowerUpTimerText;
	public GameObject[] AttractionPowerUp;
	
	// Timer
	public Text Time;
	

    private int _coinsCount;
	private int _maxCoins;
	private int _totalCoinsCollected;
	private int _totalCoins;
	private int _remainingLives;
	private int _livesLost;
	private bool _additionalLife;
	private bool _gameover;
	private CameraShake _camShake;
	private bool _stopGame;
	private int _timeAfterCollision;
    

    // Initialization
    void Start ()
	{
		_coinsCount = 0;
		_maxCoins = 5;
		_totalCoinsCollected = 0;
		_totalCoins = TotalCoins.transform.childCount;
		SetCountText();
		_remainingLives = 3;
		_livesLost = 0;
		_additionalLife = false;
		_gameover = false;
		_timeAfterCollision = 0;
        _camShake = AppManager.gameObject.GetComponent<CameraShake>();
		_stopGame = false;
		StartCoroutine(StartTimer());
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
	
	// Sets the text string specifying the number of collected coins
	public void SetCountText()
	{
		CoinsCountText.text = _coinsCount.ToString();
	}
	
	void LateUpdate()
	{
		// If the two magnets collide, remove 1 heart
		if (transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().JumpedUp &&
		    transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().JumpedUp)
		{	
			// Disable keyboard input
			transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().SetInput(false);
			transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().SetInput(false);
			
			// Create sparks
            Invoke("ImpactEffect", 0.4f);
			
			//Remove a life and gameover in case lives = 0
            RemoveLife();

            if (!_gameover)
			{
				transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().JumpedUp = false;
				transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().JumpedUp = false;

                Invoke("BringMagnetsDown", 1.5f);
			}
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
        transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().ChangePos = false;
		transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().ChangePos = false;
		
		// Enable keyboard input
		Invoke("EnableKeyboardInput", 0.8f);
	}

	void EnableKeyboardInput()
	{
		transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().SetInput(true);
		transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().SetInput(true);
	}

	// Enable attraction power-up
	public void Attraction()
	{
		AttractionPowerUp[0].SetActive(true);
		AttractionPowerUp[1].SetActive(true);
		AttractionPowerUp[2].SetActive(true);
		PowerUpTimerText.SetActive(true);
		StartCoroutine(StartCountdown(PowerUpDuration, AttractionPowerUp));
	}
	    
	// Timer for power-ups
	IEnumerator StartCountdown(int countdownValue, GameObject[] powerUp)
	{
		PowerUpTimerText.transform.GetComponent<Text>().text = ":" + countdownValue;
        
		while (countdownValue > 0)
		{
			yield return new WaitForSeconds(1.0f);
			countdownValue--;
			PowerUpTimerText.transform.GetComponent<Text>().text = ":" + countdownValue;
		}
        
		PowerUpTimerText.SetActive(false);

		for (var i = 0; i < powerUp.Length; i++)
		{
			powerUp[i].SetActive(false);
		}
	}
	
	// Timer for the level
	IEnumerator StartTimer()
	{
		var seconds = 0;
		var minutes = 0;

		Time.text = "0.00";
		
		while (!_stopGame)
		{
			while (_timeAfterCollision != 0)
			{
				yield return new WaitForSeconds(0.05f);
				seconds ++;
				_timeAfterCollision--;
				
				if (seconds < 10)
				{
					Time.text = minutes + ".0" + seconds;
				}
				else if (seconds == 60)
				{
					minutes++;
					seconds = 0;

					Time.text = minutes + ".0" + seconds;
				}
				else
				{
					Time.text = minutes + "." + seconds;
				}
			}
			
			yield return new WaitForSeconds(1.0f);
			seconds++;
			
			if (seconds < 10)
			{
				Time.text = minutes + ".0" + seconds;
			}
			else if (seconds == 60)
			{
				minutes++;
				seconds = 0;

				Time.text = minutes + ".0" + seconds;
			}
			else
			{
				Time.text = minutes + "." + seconds;
			}
		}	
		
		TimerText.transform.GetComponent<Text>().text = Time.text;
		TotalCoinsCollected.text = _totalCoinsCollected + "/" + _totalCoins;
		TotalLivesLost.text = _livesLost.ToString();
		Statistics.SetActive(true);
	}

	// Add 20 seconds to the total time after every collision with a branch or a bot
	public void SetTimeAfterCollision()
	{
		_timeAfterCollision = 20;
	}

	// Displays the statistics on the screen at the end of a level
	public void ShowStatistics()
	{
		_stopGame = true;
		
		// Disable keyboard input
		transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().SetInput(false);
		transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().SetInput(false);
	}
}
