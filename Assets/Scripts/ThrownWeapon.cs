using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
	public float damage;
	
	[Tooltip("Impulse that will be applied to the rigidbody that the collider is attached.")]
	public float impact;
	public float stabSpeedTreshold;

	private Rigidbody rb;

	private GameObject hitEffect, bloodEffect, woodEffect;

	bool flag;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		bloodEffect = (GameObject)Resources.Load("BloodHitEffect");
		woodEffect = (GameObject)Resources.Load("WoodHitEffect");
		hitEffect = (GameObject)Resources.Load("HitEffect");
	}


	private void OnTriggerEnter(Collider other)
	{
		if (flag)
			return;
		

		if (other.TryGetComponent(out MaterialData mat) && mat.CanBeStabbed && rb.velocity.magnitude >= stabSpeedTreshold)
		{

			if (mat.material == Material.Wood)
			{
				gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Ground");
				var p = Instantiate(woodEffect);
				p.transform.position = other.ClosestPointOnBounds(transform.position);
			}

			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
			rb.isKinematic = true;
			transform.SetParent(other.transform, true);

			if (!other.gameObject.isStatic)
				foreach (Collider c in gameObject.GetComponentsInChildren<Collider>())
					c.enabled = false;
			
			if (other.attachedRigidbody != null)
			{
				other.attachedRigidbody.AddForceAtPosition(impact * rb.velocity, transform.position, ForceMode.Impulse);
				bool hasHealth = other.attachedRigidbody.TryGetComponent(out Health health);
				bool hasAI = other.attachedRigidbody.TryGetComponent(out EnemyAI ai);
				if ((!hasAI && hasHealth) || (hasAI && ai.vulnerable))
					health.ReceiveDamage(damage);
				if (mat.material == Material.Flesh && ai.vulnerable)
				{
					var p = Instantiate(bloodEffect);
					p.transform.position = other.ClosestPointOnBounds(transform.position);
					p.transform.parent = other.transform;
				}
				else if (mat.material == Material.Flesh && !ai.vulnerable)
				{
					var c = Instantiate(hitEffect);
					c.transform.position = other.ClosestPointOnBounds(transform.position);
					c.transform.rotation = other.transform.rotation;
				}
			}
			flag = true;
		}
		else
        {
            var c = Instantiate(hitEffect);
            c.transform.position = other.ClosestPointOnBounds(transform.position);
            c.transform.rotation = other.transform.rotation;
			foreach (Collider col in GetComponents<Collider>())
				if(col.isTrigger)
					col.enabled = false;
        }

        
	}
}
