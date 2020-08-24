using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHurtShake : MonoBehaviour
{
	public Health health;
	public float damageShakeThreshold = 10;

	private CinemachineImpulseSource imp;

	private void Start()
	{
		imp = GetComponent<CinemachineImpulseSource>();
	}

	private void OnEnable()
	{
		health.DamageReceived += Health_DamageReceived;
	}

	private void OnDisable()
	{
		health.DamageReceived -= Health_DamageReceived;
	}

	private void Health_DamageReceived(object sender, float e)
	{
		if (e >= damageShakeThreshold)
			imp.GenerateImpulse();
	}
}
