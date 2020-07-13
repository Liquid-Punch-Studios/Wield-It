﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticleTrigger : MonoBehaviour
{
	public ParticleSystem particle;

	public void Trigger()
	{
		particle.Play(true);
	}
}
