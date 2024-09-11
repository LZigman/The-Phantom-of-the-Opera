using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
	[SerializeField] LayerMask collisionLayers; // all layers - probably default and player layer
	[SerializeField] LayerMask playerLayer; // just the player layer
	[SerializeField] float eyesightRange, eyesightFrequency; // distance of sight, every how many seconds it checks if it can see the player, we don't need to check every frame
	[SerializeField] float coneAngle; // 0 - 90

	public bool canSeePlayer;

	private void Start()
	{
		InvokeRepeating(nameof(Eyesight), eyesightFrequency, eyesightFrequency);
	}

	private void Eyesight()
	{
		Collider[] Cols = Physics.OverlapSphere(transform.position, eyesightRange, playerLayer, QueryTriggerInteraction.Ignore);

		if (Cols.Length > 0)
			PlayerIsInCone(Cols[0].transform);
		else
			canSeePlayer = false;
		
	}

	private void PlayerIsInCone(Transform player)
	{
		Vector3 playerDirection = player.position - transform.position;
		playerDirection.Normalize();

		if (Vector3.Angle(transform.forward, playerDirection) < coneAngle)
		{
			RaycastHit Hit;
			if (Physics.Raycast(transform.position, playerDirection, out Hit, eyesightRange, collisionLayers, QueryTriggerInteraction.Ignore))
			{
				if (Hit.collider.gameObject.CompareTag("Player"))
				{
					canSeePlayer = true;
				}
				else
				{
					canSeePlayer = false;
				}
			}
		}
		else
		{
			canSeePlayer = false;
		}
	}
}
