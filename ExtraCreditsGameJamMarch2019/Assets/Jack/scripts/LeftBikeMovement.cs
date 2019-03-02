using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBikeMovement : MonoBehaviour {

    Vector3 lookAt;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (InputManager.LeftTrigger() > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(InputManager.LeftTrigger() * 20 * InputManager.MainVertical(), 0, 0);
        }
    }
}
