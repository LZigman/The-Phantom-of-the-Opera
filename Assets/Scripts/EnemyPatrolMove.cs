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
	[SerializeField] float rotationSpeed = 0.5f;
	int indexPatrolPosition;

	[SerializeField] Animator animator;

	private Transform playerTransform;
	private void Start()
	{
		myAgent = GetComponent<NavMeshAgent>();
		myAgent.updateRotation = true;
		Patrol();
	}

	private void Update()
	{
		animator.SetFloat("speed", myAgent.velocity.magnitude);

		if (myState == NPCState.patrol)
			Patrol();
		if (myState == NPCState.frozen)
		{
			Vector3 lookVectorTowardsPatrolPosition = transform.position - patrolPositions[indexPatrolPosition].position;
		}
		if (myState == NPCState.looking)
		{
			Vector3 targetDir = playerTransform.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
		Debug.Log("myState = " + myState);
	}

	public void StopPatroling (bool toggle)
	{
		myAgent.isStopped = toggle;
		myState = NPCState.patrol;
	}

	private void Patrol()
	{
        animator.SetBool("isNoticing", false);

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
	public void LookAtPlayer (Transform player)
	{
		animator.SetBool("isNoticing", true);

		myState = NPCState.looking;
		playerTransform = player;
	}

	public void PlayerNoticed()
	{
        animator.SetBool("hasWon", true);
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
