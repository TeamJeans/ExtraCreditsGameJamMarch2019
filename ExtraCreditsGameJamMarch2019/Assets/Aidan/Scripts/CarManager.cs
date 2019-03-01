using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour {

	[SerializeField]
	float carVelocity = 30.0f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update (){
		// Set the car to have a constant
		rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, carVelocity);
	}
}
