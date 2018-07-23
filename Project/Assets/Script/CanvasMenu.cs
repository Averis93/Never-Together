using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour {

	public static CanvasMenu Instance { get; private set; }
	
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
}
