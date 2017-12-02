using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTime : MonoBehaviour
{	
	public GameObject Timer;
	public Camera Cam;
	
	private float ControlPointX = 950.0f;
	private float ControlPointY = 1000.0f;

	private float _startPointX;
	private float _startPointY;
	private float _endPointX;
	private float _endPointY;
	private float CurveX;
	private float CurveY;
	private float BezierTime = 0.0f;

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
	}

	void Update()
	{
		BezierTime = BezierTime + Time.deltaTime;

		if (Mathf.Approximately(BezierTime, 1.0f))
		{
			BezierTime = 0;
		}

		CurveX = (((1 - BezierTime) * (1 - BezierTime)) * _startPointX) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) +
		         ((BezierTime * BezierTime) * _endPointX);
		CurveY = (((1 - BezierTime) * (1 - BezierTime)) * _startPointY) + (2 * BezierTime * (1 - BezierTime) * ControlPointY) +
		         ((BezierTime * BezierTime) * _endPointY);
		transform.position = new Vector3(CurveX, CurveY, 0);
	}
}
