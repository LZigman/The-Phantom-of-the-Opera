using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MorfObjects
{
    BackstageBox,
    SecurityGuard,
    TrashCan,
	Default
}

public class PlayerMorfing : MonoBehaviour
{
    public MorfObjects currentMorf;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			currentMorf = MorfObjects.BackstageBox;
			Debug.Log($"Current morf: {currentMorf}");
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			currentMorf = MorfObjects.SecurityGuard;
			Debug.Log($"Current morf: {currentMorf}");
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			currentMorf = MorfObjects.TrashCan;
			Debug.Log($"Current morf: {currentMorf}");
		}
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			currentMorf = MorfObjects.TrashCan;
			Debug.Log($"Current morf: {currentMorf}");
		}

		// movement input
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.forward * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.back * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * Time.deltaTime;
		}
	}
}
