using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorfObject : MonoBehaviour
{
	[SerializeField] private DisguiseType objectType;
    [SerializeField] private float triggerRadius = 2.25f;
	private void Start()
	{
		GetComponent<SphereCollider>().radius = triggerRadius;
	}
	private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<Player>().SetCloseToMorph(true, objectType);
        }
    }

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().SetCloseToMorph(false, objectType);
        }
    }
}
