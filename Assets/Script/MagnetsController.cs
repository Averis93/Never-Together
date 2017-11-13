using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetsController : MonoBehaviour {
	
	// Coins count string
	public Text CoinsCountText;
	
	// Lives
	public GameObject[] Lives;
	public GameObject AdditionalLife;

	// GameOver text
	public GameObject GameOver;

    // Particle effect
    public GameObject[] ParticleEffect;

    // Handle Camera shake
    public float CamShakeAmt = 0.1f;
    public GameObject AppManager;
	
	// Handle power-ups
	public int PowerUpDuration = 10;
	public GameObject TimerText;
	public GameObject[] AttractionPowerUp;
	

    private int _coinsCount;
	private int _maxCoins;
	private int _remainingLives;
	private bool _additionalLife;
	private bool _gameover;
	private CameraShake _camShake;
    

    // Initialization
    void Start ()
	{
		_coinsCount = 0;
		_maxCoins = 5;
		SetCountText();
		_remainingLives = 3;
		_additionalLife = false;
		_gameover = false;

        _camShake = AppManager.gameObject.GetComponent<CameraShake>();
	}
	
	// Sets the number of collected coins
	public void SetCount()
	{
		_coinsCount++;
		
		// If the player collects 200 coins, add a life and reset the coins count
		if (_coinsCount == _maxCoins)
		{
			_coinsCount = 0;

			if (_remainingLives == 3)
			{
				AdditionalLife.SetActive(true);
				_additionalLife = true;
			}
			else
			{
				AddLife();
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
	void AddLife()
	{
		Lives[_remainingLives].SetActive(true);
		_remainingLives = _remainingLives + 1;
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

	public void Attraction()
	{
		AttractionPowerUp[0].SetActive(true);
		AttractionPowerUp[1].SetActive(true);
		AttractionPowerUp[2].SetActive(true);
		TimerText.SetActive(true);
		StartCoroutine(StartCountdown(PowerUpDuration, AttractionPowerUp));
	}
	    
	// Timer for power-ups
	IEnumerator StartCountdown(int countdownValue, GameObject[] powerUp)
	{
		TimerText.transform.GetComponent<Text>().text = ":" + countdownValue;
        
		while (countdownValue > 0)
		{
			yield return new WaitForSeconds(1.0f);
			countdownValue--;
			TimerText.transform.GetComponent<Text>().text = ":" + countdownValue;
		}
        
		TimerText.SetActive(false);

		for (var i = 0; i < powerUp.Length; i++)
		{
			powerUp[i].SetActive(false);
		}
	}
}
