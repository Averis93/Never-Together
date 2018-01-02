using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalTime : MonoBehaviour
{	
	private float _finalPosY;
	private Vector3 _tempPosText;
	private Vector3 _tempPosImg;
	private Transform _timeText;
	private Transform _timeImg;
	
	void Start ()
	{	
		_timeText = transform.GetChild(0);
		_timeImg = transform.GetChild(1);
		
		_tempPosText = _timeText.localPosition;
		_tempPosImg = _timeImg.localPosition;
		_finalPosY = _tempPosText.y + 40.0f;
	}

	void Update()
	{
		_tempPosText += new Vector3(0.0f, 5.0f, 0.0f) * Time.deltaTime * 40.0f;
		_tempPosImg += new Vector3(0.0f, 5.0f, 0.0f) * Time.deltaTime * 40.0f;
		_timeText.localPosition = _tempPosText;
		_timeImg.localPosition = _tempPosImg;

		if (_tempPosText.y >= _finalPosY)
		{
			StartCoroutine(FadeOut());
		}
	}

	IEnumerator FadeOut()
	{
		var textColor = _timeText.GetComponent<Text>().color;
		var imgColor = _timeImg.GetComponent<Image>().color;
		
		while (textColor.a > 0.0f || imgColor.a > 0.0f)
		{
			if (textColor.a > 0.0f)
			{
				textColor = new Color(textColor.r, textColor.g, textColor.b, textColor.a - (Time.deltaTime));
				_timeText.GetComponent<Text>().color = textColor;
			}

			if (imgColor.a > 0.0f)
			{
				imgColor = new Color(imgColor.r, imgColor.g, imgColor.b, imgColor.a - (Time.deltaTime));
				_timeImg.GetComponent<Image>().color = imgColor;
			}
			
			yield return null;
		}
		
		Destroy(gameObject);
	}
}