using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizzontalMovement : Movement {

    public override void move(GameObject g)
    {
        //g.transform.position += g.transform.right * 0.05f;
    }
}

public class FixedMovement : Movement
{
    public override void move(GameObject g) { }
}
