﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightBikeMovement : MonoBehaviour {

    public Animator cycleAnim;


    Vector3 speedReduct;
    int MvSpeed;
    float speed;
    float pedalSpeed;
    float BpedalSpeed;

    bool LPedal;
    bool RPedal;

    bool LPedalB;
    bool RPedalB;

    // Use this for initialization
    void Start()
    {
        speedReduct.x = 50;
        speedReduct.y = 50;
        speedReduct.z = 50;

        speed = 5.0f;
        MvSpeed = 25;

        BpedalSpeed = 0;
        pedalSpeed = 0;
        LPedal = false;
        RPedal = false;
        LPedalB = false;
        RPedalB = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

        
        //left trigger
        if (InputManager.LeftTrigger2() > 0) LPedal = true;
        if (InputManager.LeftTrigger2() == 0 && LPedal)
        {
            pedalSpeed += 0.3f;
            BpedalSpeed = 0;
            LPedal = false;
        }
        //right trigger
        if (InputManager.RightTrigger2() > 0) RPedal = true;
        if (InputManager.RightTrigger2() == 0 && RPedal)
        {
            pedalSpeed += 0.3f;
            BpedalSpeed = 0;
            RPedal = false;
        }

        //left bumper for reversing
        if (!InputManager.LeftBumper2()) LPedalB = true;
        if (InputManager.LeftBumper2() && LPedalB)
        {
            BpedalSpeed -= 0.3f;
            pedalSpeed = 0;
            LPedalB = false;
        }
        //right bumper for reversing
        if (!InputManager.RightBumper2()) RPedalB = true;
        if (InputManager.RightBumper2() && LPedalB)
        {
            BpedalSpeed -= 0.3f;
            pedalSpeed = 0;
            RPedalB = false;
        }


        //stop it going the wrong direction by itself
        if (BpedalSpeed > 0)
        {
            BpedalSpeed = 0;
        }
        if (pedalSpeed < 0)
        {
            pedalSpeed = 0;
        }

        if (BpedalSpeed < -2)
        {
            BpedalSpeed = -2;
        }
        if (pedalSpeed > 2)
        {
            pedalSpeed = 2;
        }

        //animation
        cycleAnim.SetFloat("CycleSpeed", 8 * pedalSpeed);

        //add force to move
        GetComponent<Rigidbody>().AddRelativeForce(MvSpeed * -pedalSpeed, 0, 0);
        GetComponent<Rigidbody>().AddRelativeForce(MvSpeed * -BpedalSpeed, 0, 0);




        //if (InputManager.RightTrigger() > 0)
        //{
        //    pedalSpeed++;
        //    //transform.Rotate(0, InputManager.LeftTrigger()/2, 0);
        //}


        //GetComponent<Rigidbody>().AddRelativeForce(InputManager.MainVertical() * MvSpeed * InputManager.LeftTrigger(), 0, 0);

        ////small extra force from other one
        //if (InputManager.RightTrigger() > 0)
        //{
        //    GetComponent<Rigidbody>().AddRelativeForce(InputManager.RightTrigger() + 5, 0, 0);
        //}

        //max speed
        if (GetComponent<Rigidbody>().velocity.magnitude > 100)
        {
            GetComponent<Rigidbody>().AddRelativeForce(-GetComponent<Rigidbody>().velocity);
        }
        pedalSpeed -= Time.deltaTime;
    }
}
