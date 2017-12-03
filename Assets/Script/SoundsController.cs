using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip Click;


    public void ClickSoundPlay()
    {
        audioSource.PlayOneShot(Click);
    }
}
