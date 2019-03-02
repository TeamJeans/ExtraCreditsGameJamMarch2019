using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testInputManager : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.AButton1())
        {
            Debug.Log(InputManager.LeftTrigger1());
            Debug.Log(InputManager.RightTrigger1());
        }
        if (InputManager.AButton2())
        {
            Debug.Log(InputManager.LeftTrigger2());
            Debug.Log(InputManager.RightTrigger2());
        }
    }
}
