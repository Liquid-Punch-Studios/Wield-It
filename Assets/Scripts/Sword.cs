using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[SelectionBase]
public class Sword : MonoBehaviour
{
	public GameObject user;
	private Stamina stamina;

	public Vector3 weaponHandle;
	
	public float damageFactor = 0.5f;
	public float damageSpeedTreshold = 25;

	public float staminaDrainFactor = 1.5f;

	private GameObject hitParticle;
	private GameObject bloodHitParticle;
	private GameObject woodParticle;
	public GameObject thrownWeaponPrefab;
	public bool throwable = false;

	// How much force we need for handling the weapon
	//public float force;
	//public float angularForce;

	private GameObject damageIndicator;

	private Rigidbody handlerRb;
	private Rigidbody weaponRb;
	private ConfigurableJoint weaponJoint;

	private void Start()
	{
		damageIndicator = (GameObject)Resources.Load("DamageIndicator");
		bloodHitParticle = (GameObject)Resources.Load("BloodHitEffect");
		hitParticle = (GameObject)Resources.Load("HitEffect");
		woodParticle = (GameObject)Resources.Load("WoodHitEffect");
		stamina = user.GetComponent<Stamina>();
		weaponRb = GetComponent<Rigidbody>();
		handlerRb = user.GetComponent<Rigidbody>();
		weaponJoint = GetComponent<ConfigurableJoint>();
	}

	private void FixedUpdate()
	{
		var relative = weaponRb.velocity - handlerRb.velocity;
		// Check weapon velocity relative to the player itself so we don't lose stamina by dashing etc.
		if (relative.sqrMagnitude > damageSpeedTreshold * damageSpeedTreshold)
		{
			stamina.UseStamina(relative.magnitude * staminaDrainFactor * Time.fixedDeltaTime);
		}
	}

	Dictionary<GameObject, int> triggerTracker = new Dictionary<GameObject, int>();

	private void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody == null)
			return;

		var hasAI = other.attachedRigidbody.TryGetComponent(out EnemyAI ai);
		var hasHealth = other.attachedRigidbody.TryGetComponent(out Health health);
		if (other.attachedRigidbody.gameObject != user && hasHealth)
		{
			
			if (triggerTracker.ContainsKey(other.gameObject))
			{
				if (triggerTracker[other.gameObject]++ > 0)
					return;
			}
			else
				triggerTracker.Add(other.gameObject, 1);
			if (other.attachedRigidbody.TryGetComponent<Rigidbody>(out Rigidbody otherRb))
			{
				var relative = weaponRb.velocity - otherRb.velocity;
				if (relative.sqrMagnitude > damageSpeedTreshold * damageSpeedTreshold)
				{
					GameObject p;

					if ((!hasAI && hasHealth) || (hasAI && ai.vulnerable))
                    {
						var damage = damageFactor * relative.magnitude;
						health.ReceiveDamage(damage);
						IndicateDamage(damage, other.attachedRigidbody.transform.Find("DISpawn"));
						var particleKind = bloodHitParticle;
						if(other.TryGetComponent(out MaterialData mat))
							particleKind = hitParticle;
							/*
							switch (mat.material)
							{
								case Material.Wood:
									particleKind = woodParticle;
									break;

								case Material.Metal:
									particleKind = hitParticle;
									break;
							}*/

						p = Instantiate(particleKind);
						
					}
                    else
                    {
						p = Instantiate(hitParticle);
						
					}
						
					
					var clash = other.ClosestPoint(transform.position);
					
					p.transform.position = clash;
					p.transform.parent = other.attachedRigidbody.transform;
					otherRb.AddForce(weaponRb.velocity * 1000);
				}
			}
			else
			{
				if (weaponRb.velocity.sqrMagnitude > damageSpeedTreshold * damageSpeedTreshold && hasAI && ai.vulnerable)
				{
					health.ReceiveDamage(damageFactor * weaponRb.velocity.magnitude);
					Debug.Log((int)(damageFactor * weaponRb.velocity.magnitude));
					var clash = other.ClosestPoint(transform.position);
					var p = Instantiate(hitParticle);
					p.transform.position = clash;
					//weaponRb.AddForce(weaponRb.velocity * -500);
				}
			}
		}
	}

	void IndicateDamage(float damage , Transform DISpawn)
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

	private void OnTriggerStay(Collider other)
	{
		
	}

	private void OnTriggerExit(Collider other)
	{
		if (triggerTracker.ContainsKey(other.gameObject))
			triggerTracker[other.gameObject]--;
	}
}
