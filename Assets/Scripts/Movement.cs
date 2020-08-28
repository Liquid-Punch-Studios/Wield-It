using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[HideInInspector]
	public float move;

	public Animator animator;

	public float moveSpeed = 8;
	public float moveForce = 5000;

	public float jumpSpeed = 12;
	public float jumpCost = 10;
	public float coyoteTime = 0.1f;
	public AudioPlayer jumpAudio;
	public AudioPlayer landAudio;

	// TODO: Decide if control percent for air is better
	public float airMoveSpeed = 5;
	public float airMoveForce = 5000;

	public float airJumpSpeed = 12;
	public float airJumpCost = 25;
	public int airJumpCount = 1;
	public AudioPlayer airJumpAudio;
	private int airJumpLeft;
	public int AirJumpLeft
	{
		get { return airJumpLeft; }
	}

	public float dashSpeed = 25;
	public float dashSpeedMin = 10;
	public float dashCost = 50;
	public AudioPlayer dashAudio;
	private bool dashing;
	public bool Dashing
	{
		get { return dashing; }
	}

	public float slamSpeed = 15;
	public float slamSpeedMin = 10;
	public float slamCost = 50;
	public AudioPlayer slamAudio;
	private bool slamming;
	public bool Slamming
	{
		get { return slamming; }
	}

	private int onGroundDepth = 0;
	public int OnGroundDepth
	{
		get => onGroundDepth;
		set
		{
			var old = onGroundDepth;
			if (old != value)
			{
				onGroundDepth = value;
				if (old > 0 ^ value > 0)
					OnOnGroundChanged(EventArgs.Empty);
				OnOnGroundDepthChanged(EventArgs.Empty);
			}
		}
	}
	public event EventHandler OnGroundDepthChanged;
	protected virtual void OnOnGroundDepthChanged(EventArgs e)
	{
		OnGroundDepthChanged?.Invoke(this, EventArgs.Empty);
	}

	public bool OnGround
	{
		get { return OnGroundDepth > 0 || Time.time - lastGround < coyoteTime; }
	}
	public event EventHandler OnGroundChanged;
	protected virtual void OnOnGroundChanged(EventArgs e)
	{
		OnGroundChanged?.Invoke(this, EventArgs.Empty);
	}

	public LayerMask groundLayerMask;

	private Rigidbody rb;
	private Stamina stamina;
	private Health health;
	public bool Jump()
	{
		Vector2 vel = rb.velocity; // Don't touch X axis
		if (OnGround && stamina.UseStamina(jumpCost))
		{
			vel.y = jumpSpeed;
			rb.velocity = vel;
			animator.SetTrigger("jump");
			jumpAudio?.PlayRandom(0.1f);
		}
		else if (airJumpLeft > 0 && stamina.UseStamina(airJumpCost))
		{
			vel.y = airJumpSpeed;
			rb.velocity = vel;
			airJumpLeft--;
			animator.SetTrigger("double jump");
			airJumpAudio?.PlayRandom(0.1f);
		}
		else
			return false;

		return true;
	}

	public void Hurt()
    {
		animator.SetTrigger("hurt");
		if (transform.Find("HurtSounds").TryGetComponent(out AudioPlayer a))
			a.PlayRandom(0.1f);
	}

	private float lastDashDir;

	public bool Dash(float direction)
	{
		if (stamina.UseStamina(dashCost))
		{
			rb.velocity = Vector3.right * (lastDashDir = Mathf.Sign(direction)) * dashSpeed;
			animator.SetTrigger("dash");
			dashAudio?.PlayRandom(0.1f);
		}
		else
			return false;

		return dashing = true;
	}

	public bool Slam()
	{
		if (!OnGround && stamina.UseStamina(slamCost))
		{
			rb.velocity = Vector3.down * slamSpeed;
			animator.SetBool("slam", true);
			slamAudio?.PlayRandom(0.1f);
			return slamming = true;
		}
		else
			return false;
	}

	public void ResetState()
	{
		OnGroundDepth = 0;
		dashing = slamming = false;
		move = 0;
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		stamina = GetComponent<Stamina>();
		health = gameObject.GetComponent<Health>();
		health.HpChanged += Health_HpChanged;
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
        {
			slamming = false;
			animator.SetBool("slam", false);
		}
			

		animator.SetFloat("speed X", Mathf.Abs(rb.velocity.x / moveSpeed));
		animator.SetFloat("speed Y", rb.velocity.y);
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((1 << other.gameObject.layer & groundLayerMask.value) != 0)
		{
			OnGroundDepth++;
			if (OnGroundDepth == 1)
				landAudio?.PlayRandom(0.1f);

			if (OnGround)
			{
				if (slamming && other.TryGetComponent<SlamBreak>(out SlamBreak breaker))
				{
					breaker.Break();
					rb.AddForce(100f * Vector3.down, ForceMode.Impulse);
					OnGroundDepth--;
				}

				airJumpLeft = airJumpCount;
				slamming = false;
				animator.SetTrigger("fell");
			}
		}
	}

	private float lastGround;

	private void OnTriggerExit(Collider other)
	{
		if ((1 << other.gameObject.layer & groundLayerMask.value) != 0)
		{
			OnGroundDepth--;

			if (!OnGround)
			{
				lastGround = Time.time;
				animator.ResetTrigger("fell");
			}
		}
	}


    private void Health_HpChanged(object sender, System.EventArgs e)
    {
		Hurt();
    }
}
