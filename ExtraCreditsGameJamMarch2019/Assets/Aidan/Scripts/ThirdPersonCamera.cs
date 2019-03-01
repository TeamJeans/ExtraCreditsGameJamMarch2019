using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	[SerializeField]
	Transform target;

	[SerializeField]
	float sensitivityX = 1f;
	[SerializeField]
	float sensitivityY = 1f;

	[SerializeField]
	float maxViewAngle = 50f;
	[SerializeField]
	float minViewAngle = 0f;

	[SerializeField]
	bool invertY;
	[SerializeField]
	bool stationaryCamera;

	Camera cam;

	[SerializeField]
	Vector3 offset;

	float currentX = 0f;
	float currentY = 0f;

	void Start()
	{
		cam = Camera.main;
	}

	void Update()
	{
		//currentX += Input.GetAxis("RightJoystickHorizontal") * sensitivityX;
		//if (invertY)
		//{
		//	currentY += Input.GetAxis("RightJoystickVertical") * sensitivityY;
		//}
		//else
		//{
		//	currentY -= Input.GetAxis("RightJoystickVertical") * sensitivityY;
		//}

		currentY = Mathf.Clamp(currentY, minViewAngle, maxViewAngle);
	}

	void LateUpdate()
	{
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);
		if (!stationaryCamera)
		{
			transform.position = target.position + rotation * offset;
		}
		else
		{
			transform.position = target.position + offset;
		}

		if (transform.position.y < target.position.y - .5f)
		{
			transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
		}

		transform.LookAt(target.position);
	}
}
