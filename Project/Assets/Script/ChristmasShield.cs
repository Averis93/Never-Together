using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasShield : MonoBehaviour {

	private InLevelManager _appManager;
	
	// Use this for initialization
	void Start ()
	{
		_appManager = GameObject.Find("Application Manager").GetComponent<InLevelManager>();
	}

	public void BlinkShield()
	{
		StartCoroutine(Blink(4, 0.4f, 0.4f));
	}

	IEnumerator Blink(int nTimes, float timeOn, float timeOff)
	{
		while (nTimes > 0)
		{
			GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(timeOn);
			GetComponent<Renderer>().enabled = false;
			yield return new WaitForSeconds(timeOff);
			nTimes--;
		}
		
		gameObject.SetActive(false);
		_appManager.ShieldActive = false;
	}
}
