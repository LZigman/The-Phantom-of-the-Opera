using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public enum NPCState
{
	frozen,
	patrol
}

public class EnemyPatrolMove : MonoBehaviour
{
	public NPCState myState;
	NavMeshAgent myAgent;

	[SerializeField] Transform[] patrolPositions;
	[SerializeField] float waitAtPatrolSeconds, patrolStopDistance;
	int indexPatrolPosition;

	[SerializeField] Animator animator;

	private void Start()
	{
		myAgent = GetComponent<NavMeshAgent>();
		myAgent.updateRotation = true;
		Patrol();
	}

	private void Update()
	{
		//animator.SetFloat("Speed", myAgent.velocity.magnitude);
		if (myState == NPCState.patrol)
			Patrol();
		if (myState == NPCState.frozen)
		{
			Vector3 lookVectorTowardsPatrolPosition = transform.position - patrolPositions[indexPatrolPosition].position;
		}
	}

	private void Patrol()
	{
		if (Vector3.Distance(transform.position, patrolPositions[indexPatrolPosition].position) > patrolStopDistance)
		{
			myAgent.SetDestination(patrolPositions[indexPatrolPosition].position);
			
		}
		else
		{
			myState = NPCState.frozen;
			StartCoroutine(waitPatrolSpot());
			indexPatrolPosition++;
			if (patrolPositions.Length <= indexPatrolPosition)
			{
				indexPatrolPosition = 0;
			}
		}
	}

	private IEnumerator waitPatrolSpot()
	{
		yield return new WaitForSeconds(waitAtPatrolSeconds);
		if (myState == NPCState.frozen)
		{
			myState = NPCState.patrol;
			Patrol();
		}
	}
}
