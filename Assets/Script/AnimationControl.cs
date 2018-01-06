using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControl : MonoBehaviour {

	public static AnimationControl Instance { get; private set; }
	
    public Animation[] MagnetsAnim;
    
    public bool _horizontalPlaying;


	void Start()
	{
		_horizontalPlaying = true;
	}
	
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "Menu")
		{
			if (!_horizontalPlaying && !MagnetsAnim[1].isPlaying)
			{
				MagnetsAnim[0].Play();
				_horizontalPlaying = true;
			}
			
			if (_horizontalPlaying && !MagnetsAnim[0].isPlaying)
			{
				MagnetsAnim[1].Play();
				_horizontalPlaying = false;
			}
		}
	}
	
	/*
	// Update is called once per frame
	void Update () {
		
	    if (SceneManager.GetActiveScene().name == "Menu")
	    {
		    Debug.Log(_horizontalAnimation);
		    StartCoroutine(AnimController());
	    }
	}

    IEnumerator AnimController()
    {
        if (_horizontalAnimation)
        {
            MagnetsAnim[0].Play();
            MagnetsAnim[1].Stop();
            yield return new WaitForSeconds(WaitTime);
            _horizontalAnimation = false;
        }
		else
        {
            MagnetsAnim[1].Play();
            MagnetsAnim[0].Stop();
            yield return new WaitForSeconds(WaitTime);
	        _horizontalAnimation = true;
        }
    }
    */
}
