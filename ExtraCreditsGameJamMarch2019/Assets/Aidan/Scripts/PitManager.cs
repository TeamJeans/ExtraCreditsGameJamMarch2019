using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitManager : MonoBehaviour {

	GameManager gm;

	void Awake()
	{
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			gm.KillPlayer();
		}
	}
}
