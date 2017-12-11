using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCoins : MonoBehaviour {

	public GameObject Coins;
	
	private float ControlPointX = 400.0f;
	private float ControlPointY = 1000.0f;

	private float _startPointX;
	private float _startPointY;
	private float _endPointX;
	private float _endPointY;
	private float CurveX;
	private float CurveY;
	private float BezierTime = 0.0f;
	private InLevelManager AppManager;

	private Vector3 _coinsCountPos;
	private Vector3 _startPos;
	
	void Start ()
	{
		_startPointX = transform.localPosition.x;
		_startPointY = transform.localPosition.y;

		_coinsCountPos = Coins.transform.position;
		Debug.Log("Coins posotion:" + _coinsCountPos);
		_endPointX = _coinsCountPos.x;
		_endPointY = _coinsCountPos.y;

		AppManager = GameObject.Find("Application Manager").GetComponent<InLevelManager>();
	}

	void Update()
	{
		BezierTime = BezierTime + Time.deltaTime;

		CurveX = (((1 - BezierTime) * (1 - BezierTime)) * _startPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) +
		         ((BezierTime * BezierTime) * _endPointX);
		CurveY = (((1 - BezierTime) * (1 - BezierTime)) * _startPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) +
		         ((BezierTime * BezierTime) * _endPointY);
		transform.position = new Vector3(CurveX, CurveY, 0.0f);

		if (transform.position.x <= 140.0f)
		{	
			for (var i = 0; i < 5; i++)
			{                        
				// Increment the coins count
				AppManager.GetComponent<InLevelManager>().SetCount(false);
			}
			
			Destroy(gameObject);
		}
	}
}
