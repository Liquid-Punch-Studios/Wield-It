﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerCamera : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	public float damping = 0.5f;
	public float lookAheadFactor = 3f;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;

	private float m_OffsetZ;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 m_LookAheadPos;
	
	void Start()
	{
		m_LastTargetPosition = (target.position + offset);
		m_OffsetZ = (transform.position - (target.position + offset)).z;
		transform.parent = null;
	}

	private void FixedUpdate()
	{
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = ((target.position + offset) - m_LastTargetPosition).x;

		bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget)
		{
			m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		}
		else
		{
			m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
		}

		Vector3 aheadTargetPos = (target.position + offset) + m_LookAheadPos + Vector3.forward * m_OffsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

		transform.position = newPos;

		m_LastTargetPosition = (target.position + offset);
	}
}
