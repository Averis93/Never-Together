using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
	private Vector3 _startScale;
	private Vector3 _tempScale;
	private float _endScale;
	private bool _shrink;
	
	// Use this for initialization
	void Start ()
	{
		_startScale = transform.localScale;
		_tempScale = transform.localScale;
		_endScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_shrink)
		{
			if (_tempScale.x >= _endScale + 0.2f)
			{
				_tempScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
				transform.localScale = _tempScale;
			}
			else
			{
				gameObject.SetActive(false);
				_shrink = false;
				transform.localScale = _startScale;
			}
		}
	}

	public void ShrinkShield()
	{
		_shrink = true;
	}
}
