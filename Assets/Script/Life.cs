using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{

	public float Speed = 12.0f;

	private float _finalScale;
	private float _maxScale = 5.4f;
	private Vector3 _tempScale;
	private bool _increase;

	void Start()
	{
		_tempScale = transform.localScale;
		_finalScale = _tempScale.x;
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
			if (_tempScale.x > _finalScale + 0.5f)
			{
				_tempScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime * Speed;
				transform.localScale = _tempScale;
			}
			else
			{
				transform.localScale = new Vector3(_finalScale, _finalScale, _finalScale);
			}
		}	
	}

	public void ChangeShape()
	{
		_increase = true;
	}
}
