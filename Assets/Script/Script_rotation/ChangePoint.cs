using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePoint : MonoBehaviour {
    public bool circol = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = transform.position;
        collision.gameObject.transform.rotation = transform.rotation;

        if (!circol)
            collision.gameObject.GetComponent<centerScript>().movement = new CircolarMovement(transform.position + transform.up * 3f, gameObject);
        else
            collision.gameObject.GetComponent<centerScript>().movement = new HorizzontalMovement();

        circol = !circol;
    }
}
