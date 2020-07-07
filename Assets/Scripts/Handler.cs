﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Handler : MonoBehaviour
{
	public Transform hand;

	public GameObject weapon;
	private Rigidbody weaponRb;
	private ConfigurableJoint weaponJoint;

	private Rigidbody playerRb;

	/// <summary>
	/// This is the point hand & weapon rotates against
	/// </summary>
	public Vector2 shoulderOffset;

	/// <summary>
	/// How much further weapon can be held from shoulder
	/// </summary>
	public float armLength = 1.5f;

	/// <summary>
	/// Controls whether the hand rotation is disabled
	/// </summary>
	public bool fixedWeaponAngle;
	// This controls whether weapon is held in a fixed angle
	// Usually used when stabbing enemies and terrain, also 
	// makes some cool air slam possible

	public float sensitivity = 0.02f;

	public void Wield(Vector2 delta)
	{
		if (delta != Vector2.zero)
		{
			Vector3 pos = hand.localPosition + (Vector3)delta;
			// Need to keep Z unchanged here as well
			if (((Vector2)pos - shoulderOffset).sqrMagnitude > armLength * armLength)
			{
				Vector2 reach = Vector2.ClampMagnitude((Vector2)pos, armLength);
				pos = hand.localPosition = new Vector3(reach.x, reach.y, pos.z);
			}
			else
				hand.localPosition = pos;

			if (!fixedWeaponAngle)
			{
				float angle = Mathf.Atan2(pos.y - shoulderOffset.y, pos.x - shoulderOffset.x);
				hand.localEulerAngles = (angle * Mathf.Rad2Deg) * Vector3.forward;
			}
		}
	}

	public void ChangeWeapon(GameObject weapon)
	{
		weaponRb = weapon.GetComponent<Rigidbody>();
		weaponJoint = weapon.GetComponent<ConfigurableJoint>();
	}

	private void Start()
	{
		playerRb = GetComponent<Rigidbody>();
		ChangeWeapon(weapon);
	}

	private void FixedUpdate()
	{
		
	}
}
