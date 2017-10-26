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

	// Coins count
	private int _count;
	
	// Remaining lives
	private int _remainingLives;

	// Initialization
	void Start ()
	{
		_count = 0;
		SetCountText();
		_remainingLives = 3;
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
			_remainingLives = _remainingLives - 1;
			Debug.Log(_remainingLives);
			Lives[_remainingLives].SetActive(false);
			transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().JumpedUp = false;
			transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().JumpedUp = false;
			
			Invoke("BringMagnetsDown", 3f);
		}
	}

	// Move the magnets back on the surface when they collide
	void BringMagnetsDown()
	{
		transform.GetChild(0).gameObject.GetComponent<CharacterBehaviour>().ChangePos = false;
		transform.GetChild(1).gameObject.GetComponent<CharacterBehaviour>().ChangePos = false;
	}
}
