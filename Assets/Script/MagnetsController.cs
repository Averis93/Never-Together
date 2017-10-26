using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetsController : MonoBehaviour {
	
	// Coins count string
	public Text CountText;
	
	// Lives
	public GameObject[] Lives;

	// GameOver text
	public GameObject GameOver;

	// Coins count
	private int _count;
	private int _remainingLives;
	private bool _gameover;

	// Initialization
	void Start ()
	{
		_count = 0;
		SetCountText();
		_remainingLives = 3;
		_gameover = false;
	}
	
	// Sets the number of collected coins
	public void SetCount()
	{
		_count++;
	}
	
	// Sets the text string specifying the number of collected coins
	public void SetCountText()
	{
		CountText.text = _count.ToString();
	}
	
	// If the two magnets collide, remove 1 heart
	void LateUpdate()
	{
		if (transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().JumpedUp &&
		    transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().JumpedUp)
		{
			RemoveLife();
			if (!_gameover)
			{
				transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().JumpedUp = false;
				transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().JumpedUp = false;
			
				Invoke("BringMagnetsDown", 3f);
			}
		}
	}
	
	// When the 2 magnets collide or when they hit a tree remove a life. If no more lives are available, gameover.
	public void RemoveLife()
	{
		_remainingLives = _remainingLives - 1;
		Debug.Log(_remainingLives);
		Lives[_remainingLives].SetActive(false);
		
		if (_remainingLives == 0)
		{
			_gameover = true;
			GameOver.SetActive(true);
		}
	}

	// Move the magnets back on the surface when they collide
	void BringMagnetsDown()
	{
		transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().ChangePos = false;
		transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().ChangePos = false;
	}
}
