using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineManager : MonoBehaviour {

	bool playerCrossed = false;
	public bool PlayerCrossed { get { return playerCrossed; } set { playerCrossed = value; } }

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			// Player has crossed the finishline
			playerCrossed = true;
		}
	}
}
