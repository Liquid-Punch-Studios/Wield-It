using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
	public float damage;
	
	[Tooltip("Impulse that will be applied to the rigidbody that the collider is attached.")]
	public float impact;
	public float stabSpeedTreshold;

	public LayerMask layerMask;

	private Rigidbody rb;

	private GameObject hitEffect, bloodEffect, woodEffect, damageIndicator;

	bool isStabbed = false;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		bloodEffect = (GameObject)Resources.Load("BloodHitEffect");
		woodEffect = (GameObject)Resources.Load("WoodHitEffect");
		hitEffect = (GameObject)Resources.Load("HitEffect");
		damageIndicator = (GameObject)Resources.Load("DamageIndicator");
	}


	private void OnTriggerEnter(Collider other)
	{
		if (isStabbed && (1 << other.gameObject.layer & layerMask.value) != 0)
        {
			if (other.GetComponent<Rigidbody>().velocity.y < 0)
				this.transform.Find("weapon_spear").GetComponent<Collider>().enabled = true;
			else
				this.transform.Find("weapon_spear").GetComponent<Collider>().enabled = false;
		}
		if (isStabbed || other.isTrigger || (1 << other.gameObject.layer & layerMask.value) != 0)
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
			isStabbed = true;

			if (other.attachedRigidbody != null)
			{
				if (other.attachedRigidbody.isKinematic)
                {
					foreach (Collider c in gameObject.GetComponentsInChildren<Collider>())
					{
						c.enabled = false;
					}
				}
				other.attachedRigidbody.AddForceAtPosition(impact * rb.velocity, transform.position, ForceMode.Impulse);
				bool hasHealth = other.attachedRigidbody.TryGetComponent(out Health health);
				bool hasAI = other.attachedRigidbody.TryGetComponent(out EnemyAI ai);
				if ((!hasAI && hasHealth) || (hasAI && ai.vulnerable))
                {
					health.ReceiveDamage(damage);
					IndicateDamage(damage, other.attachedRigidbody.transform.Find("DISpawn"));
				}
					
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
		}
		else
        {
			var c = Instantiate(hitEffect);
			c.transform.position = other.ClosestPointOnBounds(transform.position);
			c.transform.rotation = other.transform.rotation;
			foreach (Collider col in GetComponents<Collider>())
            {
				if (col.isTrigger)
					col.enabled = false;
            }
			if (this.transform.Find("weapon_spear") != null)
				this.transform.Find("weapon_spear").GetComponent<Collider>().enabled = true;
		}
	}
        
	void IndicateDamage(float damage, Transform DISpawn)
	{
		if (DISpawn == null)
			return;
		var di = Instantiate(damageIndicator);
		if (damage > 0)

			di.GetComponentInChildren<TextMeshPro>().text = "-" + (int)damage;

		else
		{
			di.transform.Find("Block").gameObject.SetActive(true);
			di.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
		}
		di.transform.position = DISpawn.transform.position;
	}
}
