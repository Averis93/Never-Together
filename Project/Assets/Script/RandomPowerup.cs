using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPowerup : MonoBehaviour
{ 
	public GameObject[] AvailablePowerups;
	public GameObject PowerupBack;
	public float WaitingTime;
	public float Speed;
	
	private InLevelManager _appManager;
	private bool _increase;
	private bool _decrease;
	private int _chosenPowerup;
	private Vector3 _tempScale;
	private Vector3 _backScale;
	private float _finalScale;
	private float _maxScale;

	void Start()
	{
		WaitingTime = 0.001f;
		Speed = 25.0f;
		_increase = false;
		_decrease = false;
		_backScale = PowerupBack.transform.localScale;
		_appManager = GameObject.Find("Application Manager").GetComponent<InLevelManager>();
		StartCoroutine(SelectRandom());
	}

	void Update()
	{
		if (_increase)
		{
			PowerupBack.GetComponent<Image>().color = Color.yellow;
			if (_tempScale.x < _maxScale)
			{
				_tempScale += new Vector3(0.05f, 0.05f, 0.05f) * Time.deltaTime * Speed;
				AvailablePowerups[_chosenPowerup].transform.localScale = _tempScale;
				_backScale += new Vector3(0.05f, 0.05f, 0.05f) * Time.deltaTime * Speed;
				PowerupBack.transform.localScale = _backScale;
			}
			else
			{
				_increase = false;
				_decrease = true;
			}
		}
		else if(_decrease)
		{
			if (_tempScale.x > _finalScale + 0.05f)
			{
				_tempScale -= new Vector3(0.05f, 0.05f, 0.05f) * Time.deltaTime * Speed;
				AvailablePowerups[_chosenPowerup].transform.localScale = _tempScale;
				_backScale -= new Vector3(0.05f, 0.05f, 0.05f) * Time.deltaTime * Speed;
				PowerupBack.transform.localScale = _backScale;
			}
			else
			{
				AvailablePowerups[_chosenPowerup].transform.localScale = new Vector3(_finalScale, _finalScale, _finalScale);
			}
		}
	}

	IEnumerator SelectRandom()
	{
		var oldPowerup = 0; 

		do
		{
			yield return new WaitForSeconds(WaitingTime);
			
			var newPowerup = Random.Range(0, 3);

			while (newPowerup == oldPowerup)
			{
				newPowerup = Random.Range(0, 3);
			}
			
			AvailablePowerups[oldPowerup].SetActive(false);
			AvailablePowerups[newPowerup].SetActive(true);

			oldPowerup = newPowerup;
			WaitingTime += 0.005f;

		} while (WaitingTime < 0.2f);

		_chosenPowerup = oldPowerup;
		_tempScale = AvailablePowerups[_chosenPowerup].transform.localScale;
		_finalScale = _tempScale.x;
		_maxScale = _finalScale * 1.3f;
		_increase = true;
		
		yield return new WaitForSeconds(1.2f);
		
		switch (_chosenPowerup)
		{
			case 0: // Attraction
				_appManager.Attraction();
				break;
					
			case 1: // Shield
				_appManager.Shield();
				break;
					
			case 2: // Slowdown
				_appManager.Slowdown();
				break;
		}
		
		_appManager.RandomPowerup.SetActive(false);
	}

	/*
	public float BackgroundUnion;
	public Transform PosInfiniteSequence;
	public GameObject LightEffect;
	public GameObject[] AvailablePowerUps;

	public float Speed;
	private Vector3 StartPos;
	private float ActualSpeed;

	void Start()
	{
		//Speed = 20;
		StartPos = PosInfiniteSequence.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (Speed > 1.5)
		{
			Speed -= 4 * Time.deltaTime;
			transform.Translate((new Vector3(0, -1, 0)) * Speed * Time.deltaTime);
		}
		else
		{
			Speed = 1.5f;
			transform.Translate((new Vector3(0, -1, 0)) * Speed * Time.deltaTime);

			//effetto di luce
			LightEffect.SetActive(true);
			StartCoroutine(FadePowerUpIn(AvailablePowerUps[0], 4f));
		}

		//StartCoroutine(SlowdownRotation());

		if (transform.position.y < BackgroundUnion)
			transform.position = StartPos;
	}

	IEnumerator FadePowerUpIn(GameObject powerup, float t)
	{
		var img = powerup.GetComponent<SpriteRenderer>();
		while (img.color.a < 1.0f)
		{
			img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + (Time.deltaTime / t));
			yield return null;
		}
		yield return new WaitForSeconds(1.0f);
	}
	*/
}
