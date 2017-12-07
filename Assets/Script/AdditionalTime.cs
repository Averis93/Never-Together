using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class AdditionalTime : MonoBehaviour
{	
	public GameObject Timer;
	
	private float ControlPointX = 950.0f;
	private float ControlPointY = 1000.0f;

	private float _startPointX;
	private float _startPointY;
	private float _endPointX;
	private float _endPointY;
	private float CurveX;
	private float CurveY;
	private float BezierTime = 0.0f;
	private InLevelManager AppManager;

	private Vector3 _timerPos;
	private Vector3 _startPos;
	
	void Start ()
	{
		//_startPos = Cam.WorldToScreenPoint(StartPoint.transform.position);
		_startPointX = transform.localPosition.x;
		_startPointY = transform.localPosition.y;

		_timerPos = Timer.transform.position;
		_endPointX = _timerPos.x;
		_endPointY = _timerPos.y;

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

		if (transform.position.x >= 1700.0f)
		{
			transform.gameObject.SetActive(false);
			AppManager.TimeAfterCollision = 20;
		}
	}
}
