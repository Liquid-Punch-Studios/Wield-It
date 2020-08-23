using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HazardTrigger : MonoBehaviour
{
	/// <summary>
	/// Controls whether the hazard is active.
	/// </summary>
	public bool active = true;

	/// <summary>
	/// Amount of damage to deal when an object is in the hazardous area (trigger).
	/// </summary>
	[Tooltip("Amount of damage to deal when an object is in the hazardous area (trigger).")]
	public float damage;

	/// <summary>
	/// Amount of force (push back) to apply when damage dealt.
	/// </summary>
	[Tooltip("Amount of force (push back) to apply when damage dealt.")]
	public float force;

	/// <summary>
	/// The direction in which the force will be applied.
	/// If zero, the force is applied away from object origin.
	/// </summary>
	[Tooltip("The direction in which the force will be applied. " +
		"If zero, the force is applied away from object origin.")]
	public Vector3 direction;

	/// <summary>
	/// A unique key for damage cooldown calculations.
	/// </summary>
	[Tooltip("A unique key for damage cooldown calculations.")]
	public string key = null;

	/// <summary>
	/// Indicates the time required to pass until the hazard can hit the same object.
	/// </summary>
	[Tooltip("Indicates the time required to pass until the hazard can hit the same object.")]
	public float cooldown = 0.5f;

	GameObject di;

    private void Start()
    {
		di = (GameObject)Resources.Load("DamageIndicator");
    }

    public void Activate()
	{
		foreach (Collider collider in GetComponents<Collider>())
			if (collider.isTrigger)
				collider.enabled = true;
	}

	public void Deactivate()
	{
		foreach (Collider collider in GetComponents<Collider>())
			if (collider.isTrigger)
				collider.enabled = false;
	}
	

	private void OnTriggerEnter(Collider other)
	{
		if (!active || cooldown != 0)
			return;

		if (other.gameObject.TryGetComponent<Health>(out Health health))
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
			{

				Instantiate(di, other.transform.Find("DISpawn").position, Quaternion.identity);
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
		if (!active || cooldown == 0)
			return;
		
		if (other.gameObject.TryGetComponent<Health>(out Health health))
		{
			if (health.ReceiveDamage(damage, key, cooldown) &&
				other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
			{

				var ins= Instantiate(di, other.transform.Find("DISpawn").position, Quaternion.identity);
				ins.GetComponentInChildren<TextMeshPro>().text = "-" + damage;
				if (direction == Vector3.zero)
				{
					rb.AddForce((other.transform.position - this.transform.position).normalized * force *5, ForceMode.Impulse);
				}
				else
				{
					rb.AddForce(direction * force, ForceMode.Impulse);
				}
			}
		}
	}
}
