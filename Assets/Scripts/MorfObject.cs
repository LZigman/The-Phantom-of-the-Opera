using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorfObject : MonoBehaviour
{
	[SerializeField] private DisguiseType objectType;

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
