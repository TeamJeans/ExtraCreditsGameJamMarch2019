using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

	[SerializeField]
	RoadEndTriggerScript startOfRoad;
	[SerializeField]
	Transform startOfRoadSpawnPosition;

	[SerializeField]
	RoadEndTriggerScript endOfRoad;
	[SerializeField]
	Transform endOfRoadSpawnPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void moveCarToOtherSideOfRoad(RoadEndTriggerScript roadSide, GameObject collidedCar)
	{
		// Check which side of the road that the car collided with and spawn the car back on the other side
		if (startOfRoad == roadSide)
		{
			collidedCar.transform.position = new Vector3(endOfRoadSpawnPosition.position.x, endOfRoadSpawnPosition.position.y, endOfRoadSpawnPosition.position.z);
		}
		else if (endOfRoad == roadSide)
		{
			collidedCar.transform.position = new Vector3(startOfRoadSpawnPosition.position.x, startOfRoadSpawnPosition.position.y, startOfRoadSpawnPosition.position.z);
		}
	}
}
