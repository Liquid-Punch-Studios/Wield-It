using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
	Idle,
	Set,
	Attacking,
	Vulnerable,
}

public class EnemyAI : MonoBehaviour
{
	public float visionRange;
	public float attackRange;

	public HazardTrigger damageTrigger;
	public GameObject deadEnemyPrefab;
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
		health.Died += Health_Died;
	}

	private void FixedUpdate()
	{
		Vector3 enemyToPlayer = player.position - transform.position;

		if (Mathf.Sign(enemyToPlayer.x) < 0)
			transform.rotation = Quaternion.LookRotation(Vector3.forward,Vector3.up);
		else
			transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);

		movement.move = 0;
		animator.SetFloat("speed X", rb.velocity.x);
		switch (state)
		{
			case AIState.Idle:
				if (Physics.Raycast(transform.position, enemyToPlayer, out RaycastHit hit, visionRange, LayerMask.GetMask("Player", "Wall", "Ground")))
				{
					if (hit.transform == player)
                    {
						state = AIState.Set;
						animator.SetTrigger("set");
					}
				}
				break;
			case AIState.Set:
				if (Physics.Raycast(transform.position, enemyToPlayer, out RaycastHit hit2, (visionRange + attackRange) / 2, LayerMask.GetMask("Player", "Wall", "Ground")))
				{
					if (hit2.transform == player)
					{
						state = AIState.Attacking;
					}
					
				}
                else
                {
					state = AIState.Idle;
				}
					
				break;
			case AIState.Attacking:
				if (enemyToPlayer.sqrMagnitude > visionRange * visionRange)
					state = AIState.Idle;
				else if (enemyToPlayer.sqrMagnitude > attackRange * attackRange)
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


	private void Health_Died(object sender, EventArgs e)
	{
		var body = Instantiate(deadEnemyPrefab);
		body.transform.SetPositionAndRotation(rb.transform.position, rb.transform.rotation);

		foreach (Rigidbody rigid in body.GetComponentsInChildren<Rigidbody>())
		{
			rigid.velocity = rb.velocity;
			rigid.AddExplosionForce(50, body.transform.position, 5, 1, ForceMode.Impulse);
		}
        Destroy(gameObject);
	}

}
