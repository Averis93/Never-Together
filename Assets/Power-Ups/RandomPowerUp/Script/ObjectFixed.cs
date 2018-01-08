using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFixed : MonoBehaviour {

    //public Transform Character;
    public float offset;

	// Use this for initialization
	void Start () {
        RenderSettings.fog = false;
    }

    // Update is called once per frame
    void Update () {
        //this.transform.position = Character.position + new Vector3(0, offset, 0);		
	}
}
