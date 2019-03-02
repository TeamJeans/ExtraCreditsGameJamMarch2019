﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBikeMovement : MonoBehaviour {

    Vector3 speedReduct;
    int MvSpeed;
    float speed;

    // Use this for initialization
    void Start()
    {
        speedReduct.x = 50;
        speedReduct.y = 50;
        speedReduct.z = 50;

        speed = 5.0f;
        MvSpeed = 25;
    }

    // Update is called once per frame
    void Update ()
    {

        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

        if (InputManager.RightTrigger() > 0)
        {
            //GetComponent<Rigidbody>().velocity = speed * InputManager.RightTrigger();
            //GetComponent<Rigidbody>().AddRelativeForce(InputManager.RightTrigger() * 5 * InputManager.SecondVertical(), 0, 0);

            GetComponent<Rigidbody>().AddRelativeForce(InputManager.SecondVertical() * MvSpeed * InputManager.RightTrigger() + 5, 0, 0);
            //transform.Rotate(0, -InputManager.RightTrigger()/2, 0);
        }

        //small extra force from other one
        if (InputManager.LeftTrigger() > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(InputManager.LeftTrigger() + 5, 0, 0);
        }

            //max speed
            if (GetComponent<Rigidbody>().velocity.magnitude > 100)
        {
            GetComponent<Rigidbody>().AddRelativeForce(-GetComponent<Rigidbody>().velocity);
        }
    }
}
