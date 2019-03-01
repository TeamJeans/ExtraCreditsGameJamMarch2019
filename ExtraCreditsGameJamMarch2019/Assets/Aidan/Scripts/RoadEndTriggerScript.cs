using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEndTriggerScript : MonoBehaviour {

	[SerializeField]
	RoadManager roadManager;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Car")
		{
			// Call the road's function to put the car back at the other side of the road
			roadManager.moveCarToOtherSideOfRoad(this, col.gameObject);
		}
	}
}
