using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {
    
    public AudioSource AudioSource;
    public AudioClip Click;

    
    public void ClickSoundPlay()
    {
        AudioSource.PlayOneShot(Click);
    }
}
