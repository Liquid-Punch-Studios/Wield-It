using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
	Idle,
	Set,
	Attacking,
	Stun,
}

public class EnemyAI : MonoBehaviour
{
	public float visionRange;
	public float attackRange;
	public float attackCooldown;
	private float lastAttackTime;

	public HazardTrigger damageTrigger;
	public GameObject deadEnemyPrefab;
	public bool vulnerable;

	private Transform player;

	private Animator animator;
	private Rigidbody rb;
	private Movement movement;
	private Health health;

	private AIState state;
	public AIState State
    {
        get { return state; }
        set { state = value; }
    }

	//NOT WORKING
	//AttackStart / AttackEnd Used Instead
	//public IEnumerable Hit()
	//{
	//	damageTrigger.Activate();
	//	yield return new WaitForSeconds(0.1f);
	//	damageTrigger.Deactivate();
	//}
	

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		movement = GetComponent<Movement>();
		health = GetComponent<Health>();
		health.Died += Health_Died;
		damageTrigger = GetComponentInChildren<HazardTrigger>();
		
		damageTrigger.Deactivate();
	}

	private void FixedUpdate()
	{
		Vector3 enemyToPlayer = player.position - transform.position;

		if (Mathf.Sign(enemyToPlayer.x) < 0)
			transform.rotation = Quaternion.LookRotation(Vector3.forward,Vector3.up);

        else
			transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);

		animator.SetFloat("speed X", Mathf.Sign(enemyToPlayer.x) * rb.velocity.x / movement.moveSpeed);


		movement.move = 0;
		switch (State)
		{
			case AIState.Idle:
				if (Physics.Raycast(transform.position, enemyToPlayer, out RaycastHit hit, visionRange, LayerMask.GetMask("Player", "Wall", "Ground")))
				{
					if (hit.transform == player)
                    {
						State = AIState.Set;
						animator.SetTrigger("set");
					}
				}
				break;
			case AIState.Set:
				if (enemyToPlayer.x < (visionRange + attackRange) / 2)
				{
					State = AIState.Attacking;
				}
                else
                {
					State = AIState.Idle;
				}
					
				break;
			case AIState.Attacking:
				if (enemyToPlayer.sqrMagnitude > visionRange * visionRange)
					State = AIState.Idle;
				else if (enemyToPlayer.sqrMagnitude > attackRange * attackRange) 
				{
					float move = Mathf.Sign(enemyToPlayer.x);
					movement.move = move;
				}
                else if (Time.fixedTime - lastAttackTime > attackCooldown)
                {
					var rnd = new System.Random();
					animator.SetInteger("attackType", rnd.Next(0, 4));
					animator.SetTrigger("attack");
					lastAttackTime = Time.fixedTime;
				}
				else if (Math.Abs(enemyToPlayer.x) < 2f)
				{
					float move = Mathf.Sign(-enemyToPlayer.x);
					movement.move = move;
				}
				break;
			case AIState.Stun:
					animator.SetBool("stun", true);
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
			rigid.AddExplosionForce(200, body.transform.position, 5, 1, ForceMode.Impulse);
		}
        Destroy(gameObject);
	}

	public void ResetState()
    {
		animator.SetBool("stun", false);
		State = AIState.Attacking;
	}

	public void AttackStart()
    {
		damageTrigger.Activate();
	}

	public void AttackEnd()
    {
		damageTrigger.Deactivate();
	}

	public void SetVulnerable()
    {
		vulnerable = true;
    }

	public void ResetVulnerable()
	{
		vulnerable = false;
	}
	private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.TryGetComponent(out Movement mov) && mov.Dashing && State != AIState.Stun)
        {
			State = AIState.Stun;
			rb.AddForce(collision.rigidbody.velocity * 8000);
		}
    }

}
