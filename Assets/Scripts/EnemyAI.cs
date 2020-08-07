using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
	Idle,
	Attacking,
	Vulnerable,
}

public class EnemyAI : MonoBehaviour
{
	public float visionRange;
	public float attackRange;

	public HazardTrigger damageTrigger;

	private Transform player;

	private Animator animator;
	private Rigidbody rb;
	private Movement movement;
	private Health health;

	private AIState state;

	public IEnumerable Hit()
	{
		damageTrigger.Activate();
		yield return new WaitForSeconds(0.1f);
		damageTrigger.Deactivate();
	}

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		movement = GetComponent<Movement>();
		health = GetComponent<Health>();
	}

	private void FixedUpdate()
	{
		Vector3 enemyToPlayer = player.position - transform.position;
		movement.move = 0;
		animator.SetFloat("speed X", rb.velocity.x);
		switch (state)
		{
			case AIState.Idle:
				if (Physics.Raycast(transform.position, enemyToPlayer, out RaycastHit hit, visionRange, LayerMask.GetMask("Player", "Wall", "Ground")))
				{
					if (hit.transform == player)
						state = AIState.Attacking;
				}
				break;
			case AIState.Attacking:
				if (enemyToPlayer.sqrMagnitude > attackRange * attackRange)
				{
					float move = Mathf.Sign(enemyToPlayer.x);
					movement.move = move;
				}
				else
				{
					animator.SetTrigger("attack");
				}
				break;
			case AIState.Vulnerable:

				break;
			default:
				break;
		}
	}
}
