using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HazardCollision : MonoBehaviour
{
	/// <summary>
	/// Controls whether the hazard is active.
	/// </summary>
	public bool active = true;

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

	public void Activate()
	{
		active = true;
	}

	public void Deactivate()
	{
		active = false;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!active || cooldown != 0)
			return;
		
		if (collision.gameObject.TryGetComponent<Health>(out Health health))
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
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
		if (!active || cooldown == 0)
			return;
		
		if (collision.gameObject.TryGetComponent<Health>(out Health health))
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
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
