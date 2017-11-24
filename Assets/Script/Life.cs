using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{

	public float Speed = 12.0f;

	private float _finalScale = 2.73f;
	private float _maxScale = 5.4f;
	private Vector3 _tempScale;
	private bool _increase;

	void Start()
	{
		_increase = true;
		_tempScale = transform.localScale;
	}

	// Update is called once per frame
	void Update ()
	{
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
