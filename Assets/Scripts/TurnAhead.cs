using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TurnAhead : MonoBehaviour
{
	public Rigidbody followTarget;
	public float turnFactor;
	public float damping;
	private float turnVelocity;

	void Update()
	{
		var rot = transform.localRotation.eulerAngles;
		rot.y = Mathf.SmoothDampAngle(rot.y, followTarget.velocity.x * turnFactor, ref turnVelocity, damping);
		transform.localRotation = Quaternion.Euler(rot);
	}
}
