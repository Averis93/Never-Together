using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

	public float Speed = 12.0f;

	private float _finalScale = 1.3f;
	private float _maxScale = 3.0f;
	private Vector3 _tempScale;
	private bool _increase;

	void Start()
	{
		_increase = true;
	}

	// Update is called once per frame
	void Update ()
	{
		_tempScale = transform.localScale;

		if (_increase)
		{
			if (_tempScale.x < _maxScale)
			{
				_tempScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime * Speed;
				transform.localScale = _tempScale;
			}
			else
			{
				_increase = false;
			}
		}
		else
		{
			if (_tempScale.x > _finalScale)
			{
				_tempScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime * Speed;
				transform.localScale = _tempScale;
			}
		}	
	}

	public void ChangeShape()
	{
		transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
		_increase = true;
	}
}
