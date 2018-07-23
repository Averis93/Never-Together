using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircolarMovement : Movement {
    Vector3 center;
    float distance;

    public override void move(GameObject g)
    {
        g.transform.position += g.transform.right * 0.05f;
        g.transform.position = center + (g.transform.position - center).normalized * distance;
        Vector3 dir = center - g.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        g.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }

    public CircolarMovement(Vector3 center, GameObject g)
    {
        this.center = center;
        distance = Vector3.Distance(center, g.transform.position);
    }
}
