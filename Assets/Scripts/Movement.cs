using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[HideInInspector]
	public float move;

	public float moveSpeed = 8;
	public float moveForce = 5000;

	public float jumpSpeed = 12;
	public float jumpCost = 10;
	public float coyoteTime = 0.1f;
	
	// TODO: Decide if control percent for air is better
	public float airMoveSpeed = 5;
	public float airMoveForce = 5000;

	public float airJumpSpeed = 12;
	public float airJumpCost = 25;
	public int airJumpCount = 1;
	private int airJumpLeft;
	public int AirJumpLeft
	{
		get { return airJumpLeft; }
	}
	
	public float dashSpeed = 25;
	public float dashSpeedMin = 10;
	public float dashCost = 50;
	private bool dashing;
	public bool Dashing
	{
		get { return dashing; }
	}

	public float slamSpeed = 15;
	public float slamSpeedMin = 10;
	public float slamCost = 50;
	private bool slamming;
	public bool Slamming
	{
		get { return slamming; }
	}

	private int onGround = 0;
	public bool OnGround
	{
		get { return onGround > 0 || Time.time - lastGround < coyoteTime; }
	}

	public LayerMask groundLayerMask;

	private Rigidbody rb;
	private Stamina stamina;

	public bool Jump()
	{
		Vector2 vel = rb.velocity; // Don't touch X axis
		if (OnGround && stamina.UseStamina(jumpCost))
		{
			vel.y = jumpSpeed;
			rb.velocity = vel;
		}
		else if (airJumpLeft > 0 && stamina.UseStamina(airJumpCost))
		{
			vel.y = airJumpSpeed;
			rb.velocity = vel;
			airJumpLeft--;
		}
		else
			return false;

		return true;
	}

	private float lastDashDir;

	public bool Dash(float direction)
	{
		if (stamina.UseStamina(dashCost))
			rb.velocity = Vector3.right * (lastDashDir = Mathf.Sign(direction)) * dashSpeed;
		else
			return false;

		return dashing = true;
	}

	public bool Slam()
	{
		if (!OnGround && stamina.UseStamina(slamCost))
			rb.velocity = Vector3.down * slamSpeed;
		else
			return false;

		return slamming = true;
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		stamina = GetComponent<Stamina>();
	}

	private void FixedUpdate()
	{
		// Apply some force in the direction we want to
		// move if we're not already moving in that vector with enough speed
		float speed = OnGround ? moveSpeed : airMoveSpeed;
		// Bigger the difference bigger the force
		float force = Mathf.Clamp(speed * move - rb.velocity.x, -speed, speed) / speed * moveForce;
		rb.AddForce(new Vector2(force, 0));

		if (Mathf.Abs(rb.velocity.x) < dashSpeedMin)
			dashing = false;

		if (rb.velocity.y > -slamSpeedMin)
			slamming = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((1 << other.gameObject.layer & groundLayerMask.value) != 0)
			onGround++;
		if (onGround > 0)
		{
			airJumpLeft = airJumpCount;
			slamming = false;
		}
	}

	private float lastGround;

	private void OnTriggerExit(Collider other)
	{
		if ((1 << other.gameObject.layer & groundLayerMask.value) != 0)
			onGround--;
		if (onGround < 1)
			lastGround = Time.time;
	}
}
