using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTrigger : MonoBehaviour
{
	/// <summary>
	/// Amount of damage to deal when an object is in the hazardous area (trigger).
	/// </summary>
	public float damage;

	/// <summary>
	/// Amount of force (push back) to apply when damage dealt.
	/// </summary>
	public float force;

	/// <summary>
	/// The direction in which the force will be applied.
	/// If zero, the force is applied away from object origin.
	/// </summary>
	public Vector3 direction;

	/// <summary>
	/// A unique key for damage cooldown calculations.
	/// </summary>
	public string key = null;

	/// <summary>
	/// Indicates the time required to pass until the hazard can hit the same object.
	/// </summary>
	public float cooldown = 0.5f;

	private void OnTriggerEnter(Collider other)
	{
		if (cooldown != 0)
			return;

		if (other.gameObject.GetComponent<Health>() is Health health)
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				other.gameObject.GetComponent<Rigidbody>() is Rigidbody rb)
			{
				if (direction == Vector3.zero)
				{
					rb.AddForce((other.transform.position - this.transform.position).normalized * force, ForceMode.Impulse);
				}
				else
				{
					rb.AddForce(direction * force, ForceMode.Impulse);
				}
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (cooldown == 0)
			return;
		
		if (other.gameObject.GetComponent<Health>() is Health health)
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				other.gameObject.GetComponent<Rigidbody>() is Rigidbody rb)
			{
				if (direction == Vector3.zero)
				{
					rb.AddForce((other.transform.position - this.transform.position).normalized * force, ForceMode.Impulse);
				}
				else
				{
					rb.AddForce(direction * force, ForceMode.Impulse);
				}
			}
		}
	}
}
