using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    [Header("Pause Interface")]
    public GameObject PauseInterface;

    private bool _isPause;
    private bool _showed;

    // Use this for initialization
    void Start () {
        _isPause = false;
        _showed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (_isPause)
        {
            Time.timeScale = 0f;
        }

        if (_showed)
        {
            Time.timeScale = 1f;
        }
    }

    public void PauseOn()
    {
        PauseInterface.SetActive(true);
        _isPause = true;
        _showed = false;
    }

    public void Resume()
    {
        PauseInterface.SetActive(false);
        _showed = true;
        _isPause = false;
    }
}
