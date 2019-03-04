using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBikeMovement : MonoBehaviour {

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
    void FixedUpdate ()
    {
        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);


        //pedal with left and right trigger
        if (InputManager.LeftTrigger1() > 0) LPedal = true;
        if (InputManager.LeftTrigger1() == 0 && LPedal)
        {
            pedalSpeed += 0.3f;
            BpedalSpeed = 0;
            LPedal = false;
        }

        //right trigger
        if (InputManager.RightTrigger1() > 0) RPedal = true;
        if (InputManager.RightTrigger1() == 0 && RPedal)
        {
            pedalSpeed += 0.3f;
            BpedalSpeed = 0;
            RPedal = false;
        }


        //left bumper for reversing
        if (!InputManager.LeftBumper1()) LPedalB = true;
        if (InputManager.LeftBumper1() && LPedalB)
        {
            BpedalSpeed -= 0.3f;
            pedalSpeed = 0;
            LPedal = false;
        }
        Debug.Log("Controller1: " + InputManager.LeftBumper1());
        //right bumper for reversing
        if (!InputManager.RightBumper1()) RPedalB = true;
        if (InputManager.RightBumper1() && RPedalB)
        {
            BpedalSpeed -= 0.3f;
            pedalSpeed = 0;
            RPedalB = false;
        }
        Debug.Log("Controller1: " + InputManager.RightBumper1());



        //stop it going the wrong direction by itself
        if (pedalSpeed > 4)
        {
            pedalSpeed = 4;
        }
        if (pedalSpeed < 0)
        {
            pedalSpeed = 0;
        }

        if (BpedalSpeed > 0)
        {
            BpedalSpeed = 0;
        }
        if (BpedalSpeed < -4)
        {
            BpedalSpeed = -4;
        }


        //animation
        if (pedalSpeed > 0)
        {
            cycleAnim.SetFloat("CycleSpeed", 8 * pedalSpeed);
        }
        else
        {
            cycleAnim.SetFloat("CycleSpeed", 8 * BpedalSpeed);
        }
        
        


        //add force to make object move
        GetComponent<Rigidbody>().AddRelativeForce(MvSpeed * -pedalSpeed, 0, 0);
        GetComponent<Rigidbody>().AddRelativeForce(MvSpeed * -BpedalSpeed, 0, 0);


        //max speed
        if (GetComponent<Rigidbody>().velocity.magnitude > 100)
        {
            GetComponent<Rigidbody>().AddRelativeForce(-GetComponent<Rigidbody>().velocity);
        }
        if (GetComponent<Rigidbody>().velocity.magnitude < -50)
        {
            GetComponent<Rigidbody>().AddRelativeForce(-GetComponent<Rigidbody>().velocity);
        }
        pedalSpeed -= Time.deltaTime;
        BpedalSpeed += Time.deltaTime;
    }
}
