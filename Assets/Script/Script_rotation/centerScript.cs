using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerScript : MonoBehaviour {
    public GameObject A, B;
    public int direction = 1;
    public bool change_w = false;
    public bool change_m = false;
    public bool woman = false;
    public bool man = false;

    private float dis;

    public Movement movement;

    public Vector3 pos;
	
    void Start()
    {
        movement = new FixedMovement();
    }

	void Update () {
        if (change_w)
        {
            A.transform.position += transform.up * 0.07f * direction;
            dis += 0.1f;
            if (dis > 1f)
            {
                direction *= -1;
                change_w = false;
            }
        }

        if (change_m)
        {
            B.transform.position -= transform.up * 0.07f * direction;
            dis += 0.1f;
            if (dis > 1f)
            {
                direction *= -1;
                change_m = false;
            }
        }

        if (Input.GetKeyDown("a"))
        {

            change_w = true;
            dis = 0f;
            woman = true;
        }

        if (Input.GetKeyDown("d"))
        {
            change_m = true;
            dis = 0f;
            man = true;
        }


        movement.move(gameObject);
        A.transform.position = transform.position - transform.up * direction;
        B.transform.position = transform.position + transform.up * direction;
        A.transform.rotation = transform.rotation;
        B.transform.rotation = transform.rotation;
    }
}
