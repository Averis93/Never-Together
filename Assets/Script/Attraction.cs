using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{

	private Color _color;
	private InLevelManager _appManager;

	void Start()
	{
		_color = transform.GetComponent<ParticleSystem>().startColor;
		
		_appManager = GameObject.Find("Application Manager").GetComponent<InLevelManager>();
	}

	public void DisableAttraction()
	{
		StartCoroutine(FadeOut());
	}
	
	IEnumerator FadeOut()
	{
		while (_color.a > 0.0f)
		{
			_color = new Color(_color.r, _color.g, _color.b, _color.a - (Time.deltaTime / 2.5f));
			transform.GetComponent<ParticleSystem>().startColor = _color;
			yield return null;
		}
		
		_appManager.AttractionEffect[0].SetActive(false);
		_appManager.AttractionEffect[1].SetActive(false);
	}
}
