using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    float speed = 3.0f;
    //public GameObject go;
    public ChangePoint StopMove;

    void Start() { }

    void Update()
    {
        this.transform.position += Vector3.left * Time.deltaTime * speed;
        if (StopMove.circol == false)
            this.transform.position += Vector3.left * Time.deltaTime * speed;
        else
            transform.position = transform.position;
    }
}
