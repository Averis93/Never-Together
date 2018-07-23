using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour {

	public static AudioMenu Instance { get; private set; }
	
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
