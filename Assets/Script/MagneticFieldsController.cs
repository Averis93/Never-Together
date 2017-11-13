using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldsController : MonoBehaviour {

	// Public variables
	public float MagnetStrength = 20.0f;
	public float DistanceStrength = 10.0f; // Attraction strength based on the distance
	
	// Private variables
	private Transform _coinTrans;
	private Rigidbody2D _rb;
	private Transform _magnetTrans;
	private bool _magnetInZone;
	
	void Awake ()
	{
		_magnetTrans = transform;
		_magnetInZone = false;
	}
	
	void FixedUpdate () {

		if (_magnetInZone)
		{
			float distance = Vector3.Distance(_magnetTrans.position, _coinTrans.position);
			float magnetDistanceStrength = (DistanceStrength / distance) * MagnetStrength;
			Vector3 directionToMagnet = _magnetTrans.position - _coinTrans.position;
			
			_rb.AddForce(magnetDistanceStrength * directionToMagnet);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Coin"))
		{
			_coinTrans = other.transform;
			_rb = _coinTrans.GetComponent<Rigidbody2D>();
			_magnetInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Coin"))
		{
			_magnetInZone = false;
		}
	}
}
