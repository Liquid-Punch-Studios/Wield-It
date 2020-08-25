using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
	public float damage;

	[Tooltip("Impulse that will be applied to the rigidbody that the collider is attached.")]
	public float impact;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnTriggerEnter(Collider other)
	{
		

		if (other.TryGetComponent<MaterialData>(out MaterialData mat) && mat.CanBeStabbed)
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.isKinematic = true;
			transform.SetParent(other.transform, true);
			if (other.attachedRigidbody != null)
			{
				other.attachedRigidbody.AddForceAtPosition(impact * rb.velocity, transform.position, ForceMode.Impulse);
			}
		}

		if ((other.TryGetComponent(out Health health)) && other.TryGetComponent(out EnemyAI ai) && ai.vulnerable)
		{
			health.ReceiveDamage(damage);
        }
	}
}
