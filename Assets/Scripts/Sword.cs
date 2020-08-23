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

	public GameObject hitParticle;
	public GameObject bloodHitParticle;
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
		if (other.gameObject != user && other.GetComponent<Health>() is Health health)
		{
			if (triggerTracker.ContainsKey(other.gameObject))
			{
				if (triggerTracker[other.gameObject]++ > 0)
					return;
			}
			else
				triggerTracker.Add(other.gameObject, 1);
			if (other.TryGetComponent<Rigidbody>(out Rigidbody otherRb))
			{
				var relative = weaponRb.velocity - otherRb.velocity;
				if (relative.sqrMagnitude > damageSpeedTreshold * damageSpeedTreshold)
				{
					GameObject p, di = new GameObject();
					di = Instantiate(damageIndicator);
					if (other.TryGetComponent(out EnemyAI ai) && ai.vulnerable)
                    {
						var damage = damageFactor * relative.magnitude;
						health.ReceiveDamage(damage);
						p = Instantiate(bloodHitParticle);
						di.GetComponentInChildren<TextMeshPro>().text = "-" + (int)damage;
						//Debug.Log("Damage: " + (int)(damage) + "\tHP: " + (int)health.Hp);
					}
                    else
                    {
						p = Instantiate(hitParticle);
						//di = Instantiate(damageIndicator);
						di.transform.Find("Block").gameObject.SetActive(true);
						di.GetComponentInChildren<TextMeshPro>().gameObject.SetActive(false);
					}
						
					
					var clash = other.ClosestPoint(transform.position);
					
					p.transform.position = clash;
					Debug.Log(other.gameObject.name);
					di.transform.position = other.transform.Find("DISpawn").position;
					otherRb.AddForce(weaponRb.velocity * 1000);
				}
			}
			else
			{
				if (weaponRb.velocity.sqrMagnitude > damageSpeedTreshold * damageSpeedTreshold)
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

	private void OnTriggerStay(Collider other)
	{
		
	}

	private void OnTriggerExit(Collider other)
	{
		if (triggerTracker.ContainsKey(other.gameObject))
			triggerTracker[other.gameObject]--;
	}
}
