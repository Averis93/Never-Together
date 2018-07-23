using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalCoins : MonoBehaviour {
/*
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
		Debug.Log("Coins position:" + _coinsCountPos);
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
	*/
	
	private float _finalPosY;
	private Vector3 _coinPosText;
	private Vector3 _coinPosImg;
	private Transform _coinText;
	private Transform _coinImg;
	
	void Start ()
	{	
		_coinText = transform.GetChild(0);
		_coinImg = transform.GetChild(1);
		
		_coinPosText = _coinText.localPosition;
		_coinPosImg = _coinImg.localPosition;
		_finalPosY = _coinPosText.y + 40.0f;
	}

	void Update()
	{
		_coinPosText += new Vector3(0.0f, 5.0f, 0.0f) * Time.deltaTime * 40.0f;
		_coinPosImg += new Vector3(0.0f, 5.0f, 0.0f) * Time.deltaTime * 40.0f;
		_coinText.localPosition = _coinPosText;
		_coinImg.localPosition = _coinPosImg;

		if (_coinPosText.y >= _finalPosY)
		{
			StartCoroutine(FadeOut());
		}
	}

	IEnumerator FadeOut()
	{
		var textColor = _coinText.GetComponent<Text>().color;
		var imgColor = _coinImg.GetComponent<Image>().color;
		
		while (textColor.a > 0.0f || imgColor.a > 0.0f)
		{
			if (textColor.a > 0.0f)
			{
				textColor = new Color(textColor.r, textColor.g, textColor.b, textColor.a - (Time.deltaTime));
				_coinText.GetComponent<Text>().color = textColor;
			}

			if (imgColor.a > 0.0f)
			{
				imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, imgColor.a - (Time.deltaTime));
				_coinImg.GetComponent<Image>().color = imgColor;
			}
			
			yield return null;
		}
		
		Destroy(gameObject);
	}
}
