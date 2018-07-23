using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform target; //the enemy's target
    public float moveSpeed = 1; //move speed
    public float rotationSpeed = 1; //speed of turning
    public float range = 10f;
    public float range2 = 10f;
    public float stop = 5;
    public float distance;
    public Transform myTransform; //current transform data of this enemy
     
    void Awake()
    {
        myTransform = transform; //cache transform data for easy access/preformance
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform; //target the player

    }

    void Update()
    {
        //rotate to look at the player
        distance = Vector3.Distance(myTransform.position, target.position);
        if (distance <= range2 && distance >= range)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
            Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        }
        
        else if ( distance > stop)
        {

            //move towards the player
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
            Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
        /*else if (distance <= stop)
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
            Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
        }*/


    }
}
