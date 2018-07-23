using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour {

    public float NewSpeed;

    [Header("Total Background")]
    public GameObject[] Background;

    private BackgroundMove _velocityBack;
    private BackgroundMove _velocityLastBack;


	// Use this for initialization
	void Start () {
        _velocityBack = Background[0].GetComponent<BackgroundMove>();
        _velocityLastBack = Background[1].GetComponent<BackgroundMove>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Velocity"))
        {
            _velocityBack.Speed = NewSpeed;
            _velocityLastBack.Speed = NewSpeed;
        }
    }
}
