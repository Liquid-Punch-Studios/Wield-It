using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[SelectionBase]
public class Sword : MonoBehaviour
{
	public Vector3 weaponHandle;
	
	public float damageFactor = 10f;
	public float damageSpeedTreshold = 1f;

	public float staminaDrainFactor = 0.0125f;

	// How much force we need for handling the weapon
	//public float force;
	//public float angularForce;
	
	private Player player;
	private Rigidbody playerRb;
	private Rigidbody rb;
	private ConfigurableJoint joint;

	private JointDrive drive;
	private JointDrive angularDrive;
	private JointDrive driveTired;
	private JointDrive angularDriveTired;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		var playerObj = GameObject.FindGameObjectWithTag("Player");
		player = playerObj.GetComponent<Player>();
		playerRb = playerObj.GetComponent<Rigidbody>();
		joint = GetComponent<ConfigurableJoint>();

		drive = driveTired = joint.xDrive;
		//driveTired.maximumForce *= tiredForceFactor;

		angularDrive = angularDriveTired = joint.angularYZDrive;
		//angularDriveTired.maximumForce *= tiredForceFactor;
	}

	private void FixedUpdate()
	{
		var relative = rb.velocity - playerRb.velocity;
		// Check weapon velocity relative to the player itself so we don't lose stamina by dashing etc.
		if (relative.sqrMagnitude > damageSpeedTreshold * damageSpeedTreshold)
		{
			if (player.stamina > staminaDrainFactor * rb.velocity.magnitude)
			{
				player.stamina -= staminaDrainFactor * rb.velocity.magnitude;
			}
			//else
			//{
			//	joint.xDrive = joint.yDrive = driveTired;
			//	joint.angularYZDrive = angularDriveTired;
			//}
		}
		{
			var tired = drive;
			tired.positionSpring *= player.stamina / player.maxStamina;
			joint.xDrive = joint.yDrive = tired;

			var tiredAngular = angularDrive;
			tiredAngular.positionSpring *= player.stamina / player.maxStamina;
			joint.angularYZDrive = tiredAngular;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger Enter: " + other.name);
	}

	private void OnTriggerStay(Collider other)
	{
		
	}

	private void OnTriggerExit(Collider other)
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Colission Enter: " + collision.gameObject.name);
	}
}
