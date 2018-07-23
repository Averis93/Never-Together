using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundManager : MonoBehaviour {

    public AudioSource audioSource_Bang0;
    public AudioSource audioSource_Bang1;

    [Header("Explosion sounds")]
    public AudioClip[] explosions;

    // Use this for initialization
    void Start () {
        StartCoroutine("soundController", 0.8f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator soundController()
    {
        while (true)
        {
            audioSource_Bang1.PlayOneShot(explosions[1]);
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.1f));
            audioSource_Bang0.PlayOneShot(explosions[0]);
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.1f));
            audioSource_Bang1.PlayOneShot(explosions[1]);
        }
    }

    static class CoroutineUtilities
    {
        public static IEnumerator WaitForRealTime(float timeS)
        {
            while (true)
            {
                float pauseEndTime = Time.realtimeSinceStartup + timeS;
                while (Time.realtimeSinceStartup < pauseEndTime)
                {
                    yield return 0;
                }
                break;
            }
        }
    }
}
