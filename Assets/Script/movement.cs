using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public int direction = 1;
    public bool change = false;

    public float distance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.right * 0.01f;
        if (change)
        {
            transform.position += transform.up * 0.1f * direction;
            distance += 0.1f;
            if(distance > 1f)
            {
                change = false;
                direction *= -1;
            }
        }

        if (Input.GetKeyDown("a"))
        {
            change = true;
            distance = 0f;
        }
	}
}
