using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBikeMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.RightTrigger() > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(InputManager.RightTrigger() * 20 * InputManager.SecondVertical(), 0, 0);
        }
    }
}
