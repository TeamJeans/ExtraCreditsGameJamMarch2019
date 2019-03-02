using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInputManager : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.AButton())
        {
            Debug.Log(InputManager.MainJoystick());
            Debug.Log(InputManager.LeftTrigger());
            Debug.Log(InputManager.RightTrigger());
        }
	}
}
