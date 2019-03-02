using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    //left stick
    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainHorizontal");
        r += Input.GetAxis("K_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainVertical");
        r += Input.GetAxis("K_MainVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(),0,MainVertical());
    }

    //right stick
    public static float SecondHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_SecondHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float SecondVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_SecondVertical");
        //r += Input.GetAxis("K_MainVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static Vector3 SecondJoystick()
    {
        return new Vector3(SecondHorizontal(), 0, SecondVertical());
    }

    //triggers
    public static float LeftTrigger()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_LeftTrigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float RightTrigger()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_RightTrigger");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    //buttons
    public static bool AButton()
    {
        return Input.GetButtonDown("A_Button");
    }
    public static bool BButton()
    {
        return Input.GetButtonDown("B_Button");
    }
    public static bool XButton()
    {
        return Input.GetButtonDown("X_Button");
    }
    public static bool YButton()
    {
        return Input.GetButtonDown("Y_Button");
    }
}
