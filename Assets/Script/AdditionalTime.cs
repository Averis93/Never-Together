using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalTime : MonoBehaviour
{	
	private float _finalPosY;
	private Vector3 _tempPos;
	
	void Start ()
	{	
		_tempPos = transform.localPosition;		
		_finalPosY = _tempPos.y + 40.0f;
	}

	void Update()
	{
		_tempPos += new Vector3(0.0f, 5.0f, 0.0f) * Time.deltaTime * 40.0f;
		transform.localPosition = _tempPos;

		if (_tempPos.y >= _finalPosY)
		{
			StartCoroutine(FadeOut());
		}
	}

	IEnumerator FadeOut()
	{
		var textColor = transform.GetComponent<Text>().color;
		
		while (textColor.a > 0.0f)
		{
			textColor = new Color(textColor.r, textColor.g, textColor.b, textColor.a - (Time.deltaTime));
			transform.GetComponent<Text>().color = textColor;
			yield return null;
		}
		
		Destroy(gameObject);
	}
}