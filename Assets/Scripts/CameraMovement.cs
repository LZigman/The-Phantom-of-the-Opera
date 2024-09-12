using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
	[SerializeField] private float cameraDistance = 10f;

	private void Update()
	{
		transform.position = playerTransform.position + cameraDistance * Vector3.up;
	}
}
