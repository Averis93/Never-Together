using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour {
    
    public bool isPaused;

    // Use this for initialization
    void Start()
    {
        isPaused = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            PauseGame(true);
        }
        else
        {
            PauseGame(false);
        }
        

        if (Input.GetKeyDown ("p"))
        {
            if (isPaused == false)
            {
                isPaused = true;
            }
            else
            {
                isPaused = false;
            }
        }
    }

    void PauseGame(bool state)
    {
        if (state)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
