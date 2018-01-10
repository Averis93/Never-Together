using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControl : MonoBehaviour {

	public static AnimationControl Instance { get; private set; }
	
    public Animation[] MagnetsAnim;
    
    public bool _horizontalPlaying;

    private MenuManager MenuManager;

	void Start()
	{
        MenuManager = gameObject.GetComponent<MenuManager>();        
	}
	
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "Menu")
		{
			if (!_horizontalPlaying && !MagnetsAnim[1].isPlaying)
			{
                if (!MenuManager._animOff)
                {
                    _horizontalPlaying = true;
                    MagnetsAnim[0].Play();
                    MagnetsAnim[1].Stop();
                    MenuManager._animOff = false;
                }
                else
                {
                    MagnetsAnim[1].Play();
                    MagnetsAnim[0].Stop();
                    _horizontalPlaying = false;
                    MenuManager._animOff = false;
                }
            }
			
			if (_horizontalPlaying && !MagnetsAnim[0].isPlaying)
			{
                if (!MenuManager._animOff)
                {
                    MagnetsAnim[1].Play();
                    MagnetsAnim[0].Stop();
                    _horizontalPlaying = false;
                    MenuManager._animOff = false;
                }
                else
                {
                    MagnetsAnim[1].Play();
                    MagnetsAnim[0].Stop();
                    _horizontalPlaying = true;
                    MenuManager._animOff = false;
                }
            }
        }
    }
 }
