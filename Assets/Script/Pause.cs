using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public TriggerController InputTutorial;

    [Header("Pause Interface")]
    public GameObject PauseInterface;

    public bool _isPause;
    public bool _resume;

    // Use this for initialization
    void Start () {
        _isPause = false;
        _resume = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (_isPause)
        {
            Time.timeScale = 0f;
        }

        if (_resume)
        {
            if (InputTutorial._isFreeze)
            {
                PauseInterface.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void PauseOn()
    {
        PauseInterface.SetActive(true);
        _isPause = true;
        _resume = false;
    }

    public void Resume()
    {
        PauseInterface.SetActive(false);
        _resume = true;
        _isPause = false;
    }
}
