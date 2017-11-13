using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
	public float Speed;

	private float _rightBound;
	private float _leftBound;
	private int _direction;
	
	
	// Use this for initialization
	void Start ()
	{
		Speed = 2.0f;
		_direction = 1;
		_rightBound = transform.localPosition.x + 1.5f;
		_leftBound = transform.localPosition.x - 1.5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.localPosition.x > _rightBound) {
			_direction = -1;
		}
		else if (transform.localPosition.x < _leftBound) {
			_direction = 1;
		}
		
		transform.localPosition += Vector3.right * _direction * Speed * Time.deltaTime; 
	}
}
