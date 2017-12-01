using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour {

    public float BackgroundUnion;
    //Point of restart movement
    public Transform PosInfiniteBackground;

    private Vector3 StartPos;

    void Start()
    {
        StartPos = PosInfiniteBackground.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < BackgroundUnion)
            transform.position = StartPos;
	}
}
