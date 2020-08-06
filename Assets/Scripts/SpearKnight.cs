using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
	Idle,
	Chasing,
	Vulnerable,
	Attacking,
}

public class SpearKnight : MonoBehaviour
{
	private AIState state;

	private void FixedUpdate()
	{
		
	}
}
