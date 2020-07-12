using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour
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

	private void OnCollisionEnter(Collision collision)
	{
		if (cooldown != 0)
			return;
		
		if (collision.gameObject.GetComponent<Health>() is Health health)
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				collision.gameObject.GetComponent<Rigidbody>() is Rigidbody rb)
			{
				if (direction == Vector3.zero)
				{
					Vector3 sum = new Vector3();
					for (int i = 0; i < collision.contactCount; i++)
					{
						ContactPoint contact = collision.GetContact(i);
						sum += contact.normal;
					}
					sum /= collision.contactCount;
					rb.AddForce(sum * -force, ForceMode.Impulse);
				}
				else
				{
					rb.AddForce(direction * force, ForceMode.Impulse);
				}
			}
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		if (cooldown == 0)
			return;
		
		if (collision.gameObject.GetComponent<Health>() is Health health)
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				collision.gameObject.GetComponent<Rigidbody>() is Rigidbody rb)
			{
				if (direction == Vector3.zero)
				{
					Vector3 sum = new Vector3();
					for (int i = 0; i < collision.contactCount; i++)
					{
						ContactPoint contact = collision.GetContact(i);
						sum += contact.normal;
					}
					sum /= collision.contactCount;
					rb.AddForce(sum * -force, ForceMode.Impulse);
				}
				else
				{
					rb.AddForce(direction * force, ForceMode.Impulse);
				}
			}
		}
	}
}
