using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour {

    public float UpSpeed;
    public float DownSpeed;
    public float Amplitude;

    private float _index;
    private bool Levitation;

    // Use this for initialization
    void Start () {
        Levitation = false;
	}
    
    // Update is called once per frame
    void Update()
    {
        if (Levitation)
            StartCoroutine(floatingUp());
        else if (!Levitation)
            StartCoroutine(floatingDown());
    }

    IEnumerator floatingUp()
    {
        _index += Time.unscaledDeltaTime;
        float y = Mathf.Abs(UpSpeed * Mathf.Sin(Amplitude * _index));
        transform.localPosition += new Vector3(0, y, 0);
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
        Levitation = false;
    }

    IEnumerator floatingDown()
    {
        _index += Time.unscaledDeltaTime;
        float y = Mathf.Abs(DownSpeed * Mathf.Sin(Amplitude * _index));
        transform.localPosition -= new Vector3(0, y, 0);
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
        Levitation = true;
    }

    //Necessary when tutorials are showed and the timeScale is equal to 0. 
    //Allows to keep the floating.
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
