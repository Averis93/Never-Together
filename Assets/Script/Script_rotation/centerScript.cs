using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour {
    public GameObject A, B;
    public float direction = 1;

    private float dis;

    public Movement movement;

    public Vector3 pos;
	
    void Start()
    {
        movement = new FixedMovement();
    }

	void Update () {
         movement.move(gameObject);
        A.transform.position = transform.position - transform.up * direction;
        B.transform.position = transform.position + transform.up * direction;
        A.transform.rotation = transform.rotation;
        B.transform.rotation = transform.rotation;
    }
}
