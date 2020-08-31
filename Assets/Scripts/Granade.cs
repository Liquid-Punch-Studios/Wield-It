using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Granade : MonoBehaviour
{
	public float timer = 3;

	public float damage = 100;
	public float force = 100;
	public AnimationCurve damageFallof = AnimationCurve.Linear(0, 1, 1, 0);

	public float inRadius = 5;
	public float outRadius = 5; // Additive
	public float TotalRadius { get => inRadius + outRadius; }

	public GameObject ExplosionEffect;
	public GameObject damageIndicator;
	public CinemachineImpulseSource impulse;

	public Animator animator;
	public AnimationClip steamupClip;

	void Start()
	{
		StartCoroutine(Explode());
	}

	public IEnumerator Explode()
	{
		yield return new WaitForSeconds(Mathf.Clamp(timer - steamupClip.length, 0, timer));
		animator.SetTrigger("isSet");
		yield return new WaitForSeconds(steamupClip.length);
		impulse.GenerateImpulse();
		Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
		Harm();
		Destroy(gameObject);
	}

	public void Harm()
	{
		RaycastHit[] rcs = Physics.SphereCastAll(transform.position, TotalRadius, Vector3.up);

		foreach (RaycastHit rc in rcs)
		{
			if (rc.collider.attachedRigidbody == null)
				continue;

			float distance = (transform.position - rc.transform.position).magnitude;

			Vector3 closestPoint = rc.collider.ClosestPointOnBounds(transform.position);
			float closestDistance = (closestPoint - transform.position).magnitude;
			
			if (closestDistance < inRadius && rc.transform.TryGetComponent(out SlamBreak sb))
			{
				sb.Break();
				continue;
			}

			float impact = damageFallof.Evaluate((Mathf.Clamp(closestDistance, inRadius, TotalRadius) - inRadius) / outRadius);

			var hasHealth = rc.collider.attachedRigidbody.transform.TryGetComponent(out Health health);
			var isPlayer = rc.transform == GameManager.Instance.player.transform;

			if (!isPlayer && hasHealth && impact > 0)
			{
				if (health.ReceiveDamage(damage * impact))
					IndicateDamage(damage * impact, rc.collider.attachedRigidbody.transform.Find("DISpawn"));
			}

			if (rc.transform.TryGetComponent(out Rigidbody rb))
				rb.AddExplosionForce(force * impact, transform.position, TotalRadius, 1f, ForceMode.Impulse);
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
			di.GetComponentInChildren<TMP_Text>().gameObject.SetActive(false);
		}
		di.transform.position = DISpawn.transform.position;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, inRadius);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, TotalRadius);
	}
}
