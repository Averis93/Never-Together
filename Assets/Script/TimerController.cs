using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour {

	public float TargetTime = 20.0f;
	
	private GameObject PowerUp;
	private GameObject[] MagneticFields;
 
	void Update(){
 
		TargetTime -= Time.deltaTime;
 
		if (TargetTime <= 0.0f)
		{
			TimerEnded();
		}
 
	}
 
	void TimerEnded()
	{
		PowerUp.SetActive(false);
		Destroy(gameObject);
	}
}
