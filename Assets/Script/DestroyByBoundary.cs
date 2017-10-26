using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    // Destroys every game object exiting from the boundary (screen)
	void OnTriggerExit2D(Collider2D other)
	{
		Destroy(other.gameObject);
	}
}
