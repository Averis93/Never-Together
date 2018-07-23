using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMove : MonoBehaviour {

    public float Speed = 3.0f;
    //public GameObject go;
    //public ChangePoint StopMove;

    void Start() { }

    void Update()
    {

        transform.position += Vector3.left * Time.deltaTime * Speed;
        
        /*
        if (StopMove.circol == false)
            transform.position += Vector3.left * Time.deltaTime * Speed;
        else
            transform.position = transform.position;
        */

    }
}
