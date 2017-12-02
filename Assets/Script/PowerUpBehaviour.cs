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
	void Update () {
        if (Levitation)
            StartCoroutine(floatingUp());
        else if (!Levitation)
            StartCoroutine(floatingDown());
    }

    IEnumerator floatingUp()
    {
        _index += Time.deltaTime;
        float y = Mathf.Abs(UpSpeed * Mathf.Sin(Amplitude * _index));
        transform.localPosition += new Vector3(0, y, 0);
        yield return new WaitForSeconds(1);
        Levitation = false;
    }

    IEnumerator floatingDown()
    {
        _index += Time.deltaTime;
        float y = Mathf.Abs(DownSpeed * Mathf.Sin(Amplitude * _index));
        transform.localPosition -= new Vector3(0, y, 0);
        yield return new WaitForSeconds(1);
        Levitation = true;
    }
}
