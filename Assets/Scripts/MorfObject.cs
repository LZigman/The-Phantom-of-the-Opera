using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorfObject : MonoBehaviour
{
    [SerializeField] private MorfObjects objectType;

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Player in trigger!");
			if (other.gameObject.GetComponent<Player>().isInteracting == true)
			{
				PlayerMorfing playerMorfing = other.gameObject.GetComponent<PlayerMorfing>();
				playerMorfing.MorfInto(objectType);
				other.gameObject.GetComponent<Player>().isInteracting = false;
			}
		}
	}
}
