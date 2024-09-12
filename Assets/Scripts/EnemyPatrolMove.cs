using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public enum NPCState
{
	frozen,
	patrol,
	looking
}

public class EnemyPatrolMove : MonoBehaviour
{
	public NPCState myState;
	NavMeshAgent myAgent;

	[SerializeField] Transform[] patrolPositions;
	[SerializeField] float waitAtPatrolSeconds, patrolStopDistance;
	int indexPatrolPosition;
	[SerializeField] Animator animator;
	private Transform playerTransform;
	[SerializeField] private float rotationSpeed;
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
		if (myState == NPCState.looking)
		{
			Vector3 targetDir = playerTransform.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}

	public void StopPatroling (bool toggle)
	{
		myAgent.isStopped = toggle;
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
