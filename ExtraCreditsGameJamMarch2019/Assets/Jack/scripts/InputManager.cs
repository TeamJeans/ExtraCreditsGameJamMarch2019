using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    //controller1
    public static float MainHorizontal1()
    {
        float r = 0.0f;
        r += Input.GetAxis("J1Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float MainVertical1()
    {
        float r = 0.0f;
        r += Input.GetAxis("J1Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    //triggers
    public static float LeftTrigger1()
    {
        float r = 0.0f;
        r += Input.GetAxis("J1LTrigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float RightTrigger1()
    {
        float r = 0.0f;
        r += Input.GetAxis("J1RTrigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    //bumpers
    public static bool LeftBumper1()
    {
        return Input.GetButtonDown("J1LBumper");
    }
    public static bool RightBumper1()
    {
        return Input.GetButtonDown("J1RBumper");
    }

    //buttons
    public static bool AButton1()
    {
        return Input.GetButtonDown("J1A");
    }
    public static bool BButton1()
    {
        return Input.GetButtonDown("J1B");
    }
    public static bool XButton1()
    {
        return Input.GetButtonDown("J1X");
    }
    public static bool YButton1()
    {
        return Input.GetButtonDown("J1Y");
    }







    //controller2
    public static float MainHorizontal2()
    {
        float r = 0.0f;
        r += Input.GetAxis("J2Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float MainVertical2()
    {
        float r = 0.0f;
        r += Input.GetAxis("J2Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    //triggers
    public static float LeftTrigger2()
    {
        float r = 0.0f;
        r += Input.GetAxis("J2LTrigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float RightTrigger2()
    {
        float r = 0.0f;
        r += Input.GetAxis("J2RTrigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    //bumpers
    public static bool LeftBumper2()
    {
        return Input.GetButtonDown("J2LBumper");
    }
    public static bool RightBumper2()
    {
        return Input.GetButtonDown("J2RBumper");
    }
    //buttons
    public static bool AButton2()
    {
        return Input.GetButtonDown("J2A");
    }
    public static bool BButton2()
    {
        return Input.GetButtonDown("J2B");
    }
    public static bool XButton2()
    {
        return Input.GetButtonDown("J2X");
    }
    public static bool YButton2()
    {
        return Input.GetButtonDown("J2Y");
    }


}
