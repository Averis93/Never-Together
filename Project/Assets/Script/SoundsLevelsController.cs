using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundsLevelsController : MonoBehaviour {
    
    public AudioSource audioSource_UnlockClick;
    public AudioSource audioSource_LockClick;
    public AudioClip Click;
    public AudioClip LockClick;
    
    private bool[] LockedLevel;
    private String NameButtonLevel;
    private int _currentLevel;
    

    void Update()
    {
        LockedLevel = GetComponent<LevelsManager>().Locked;
    }

    public void ClickSoundPlay()
    {
        audioSource_UnlockClick.PlayOneShot(Click);
    }    

    public void ClickSoundLevels()
    {
        NameButtonLevel = EventSystem.current.currentSelectedGameObject.name;

        if (NameButtonLevel != "BonusLevel")
        {
            _currentLevel = Int32.Parse(NameButtonLevel.Substring(NameButtonLevel.Length - 1));
            
        }else if (NameButtonLevel == "BonusLevel")
        {
            _currentLevel = 7;
        }

        if (!LockedLevel[_currentLevel - 1])
        {
            audioSource_UnlockClick.PlayOneShot(Click);
        }
        else
        {
            audioSource_LockClick.PlayOneShot(LockClick);
        }
    }
}
