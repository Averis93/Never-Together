using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    float speed = 3.0f;
    //public GameObject go;

    void Start() { }

    void Update()
    {
        Vector3 movePlayerVector = Vector3.left;
        this.transform.position += movePlayerVector * Time.deltaTime * speed;
    }
}
