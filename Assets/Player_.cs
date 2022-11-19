using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour
{
	public float moveSpeed = 5;
	public float jumpSpeed = 10;
	public Vector2 feetCenter;
	public float feetRadius;
	public LayerMask groundLayer;

	private Vector3 speed;
	private CharacterController characterController;
	private Controls controls;
	private bool onGround = false;
	private Collider[] colliderBuffer;

	public bool OnGround
	{
		get
		{
			return Physics.OverlapSphereNonAlloc(transform.TransformPoint(feetCenter), feetRadius, colliderBuffer, groundLayer) > 0;
		}
	}

	private void OnEnable()
	{
		controls.Enable();
	}

	private void Awake()
	{
		colliderBuffer = new Collider[5];
		controls = new Controls();
	}

	private void Start()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		float deltaTime = Time.fixedDeltaTime;
		
		float move = controls.Player.Move.ReadValue<float>();
		bool jump = controls.Player.Jump.triggered;
		if (jump && characterController.isGrounded)
			speed.y = jumpSpeed;
		
		speed += Physics.gravity * deltaTime;
		speed.x = move * moveSpeed;
		
		CollisionFlags flags = characterController.Move(speed * deltaTime);
		if (flags.HasFlag(CollisionFlags.Below))
		{
			if (speed.y < 0)
				speed.y = 0;
		}
		if (flags.HasFlag(CollisionFlags.Above))
		{
			if (speed.y > 0)
				speed.y = 0;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.TransformPoint(feetCenter), feetRadius);
	}

	private void OnDisable()
	{
		controls.Disable();
	}
}
