using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    public float Speed = 3.0f;
    //public GameObject go;
    public ChangePoint StopMove;

    void Start() { }

    void Update()
    {

    this.transform.position += Vector3.left * Time.deltaTime * Speed;

    if (StopMove.circol == false)
        this.transform.position += Vector3.left * Time.deltaTime * Speed;
    else
        transform.position = transform.position;

    }
}
