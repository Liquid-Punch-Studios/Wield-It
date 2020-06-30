﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	public Controls controls;

	public Vector2 shoulderOffset;  /// This is the point hand & weapon rotates against
	public float armLength = 1.5f;  /// How much further weapon can be held from shoulder

	public float wieldFactor = 0.02f; /// How much the hand moves in relation to the mouse/touch delta
	// TODO: Maybe seperate mouse & touch?
	// FIXME: Mouse simulation for touch input is not implemented in the new inout system yet

	public float moveSpeed = 4;
	public float moveForce = 100;
	public float jumpSpeed = 8;
	public float airMoveSpeed = 4;
	public float airMoveForce = 100;
	public float airJumpSpeed = 8;
	// TODO: Decide if control percent for air is better
	public int airJumpCount = 1;
	private int airJumpLeft;

	private int onGround = 0;
	public bool OnGround { get => onGround > 0; }

	// TODO: Not sure if we should restrict player to
	// only be able to jump on static ground, maybe
	// allow jumping from heads of enemies?
	public LayerMask groundLayerMask;

	private Rigidbody2D playerRb;

	public Transform hand;
	public Transform handGfx;

	private GameObject weapon;
	public GameObject Weapon
	{
		get
		{
			return this.weapon;
		}
		set
		{
			this.weapon = value;
			if (weapon != null)
			{
				//weaponData = weapon.GetComponent<Weapon>();
				weaponRb = weapon.GetComponent<Rigidbody2D>();
				weaponJoint = weapon.GetComponent<RelativeJoint2D>();
				//weaponJoint.linearOffset = (weaponData.weaponHandle * weapon.transform.localScale);
			}
			else
			{
				//weaponData = null;
				weaponRb = null;
				weaponJoint = null;
			}
		}
	}
	private Rigidbody2D weaponRb;
	private RelativeJoint2D weaponJoint;

	// This controls whether weapon is held in a fixed angle
	// Usually used when stabbing enemies and terrain, also 
	// makes some cool air slam possible
	private bool fixedWeaponAngle;

	// HACK: Need to keep track of angle because relative joint resets after [180, -180]
	// We then manually edit angular offset of the joint instead of the object it is bound to
	private float realAngularOffset;

	// Awake is called once during the lifetime of the script, on its initial awake state, prior to any other functions
	private void Awake()
	{

	}

	// Start is called before the first frame update
	private void Start()
	{
		playerRb = GetComponent<Rigidbody2D>();
		Weapon = GameObject.FindGameObjectWithTag("MainWeapon");
	}

	private void OnEnable()
	{
		if (controls == null)
			controls = new Controls();
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}

	/// Helper function for math-like positive modulus
	public static float Mod(float x, float m)
	{
		var r = x % m;
		return r < 0 ? r + m : r;
	}

	// FixedUpdate is called once per physics frame
	private void FixedUpdate()
	{
		float move = controls.Player.Move.ReadValue<float>();

		// Apply some force in the direction we want to
		// move if we're not already moving in that vector with enough speed
		// Bigger the difference bigger the force
		// FIXME: Needs fine tuning, experimental
		float speed = OnGround ? moveSpeed : airMoveSpeed;
		if (move > 0 && playerRb.velocity.x < speed * move)
		{
			float force = Mathf.Clamp(speed * move - playerRb.velocity.x, 0, speed) * moveForce;
			playerRb.AddForce(new Vector2(force, 0));
		}
		else if (move < 0 && playerRb.velocity.x > speed * move)
		{
			float force = Mathf.Clamp(speed * move - playerRb.velocity.x, -speed, 0) * moveForce;
			playerRb.AddForce(new Vector2(force, 0));
		}

		// Gamepads and some keyboards have pressure
		// sensors but we probably don't give a fuck
		bool jump = controls.Player.Up.triggered;
		bool down = controls.Player.Down.triggered;

		if (jump)
		{
			Vector2 vel = playerRb.velocity; // Don't touch X axis
			if (OnGround)
			{
				vel.y = jumpSpeed;
				playerRb.velocity = vel;
			}
			else if (airJumpLeft > 0)
			{
				vel.y = airJumpSpeed;
				playerRb.velocity = vel;
				airJumpLeft--;
			}
		}

		// Weapon movement delta
		Vector2 wield = controls.Player.Wield.ReadValue<Vector2>();
		if (wield != Vector2.zero)
		{
			Vector3 pos = hand.localPosition + (Vector3)wield * wieldFactor;
			// Need to keep Z unchanged here as well
			if (((Vector2)pos - shoulderOffset).sqrMagnitude > armLength * armLength)
			{
				Vector2 reach = Vector2.ClampMagnitude((Vector2)pos, armLength);
				pos = hand.localPosition = new Vector3(reach.x, reach.y, pos.z);
			}
			else
				hand.localPosition = pos;
			float angle = Mathf.Atan2(pos.y - shoulderOffset.y, pos.x - shoulderOffset.x);
			//Debug.Log("Pos: " + pos + "  Angle: " + angle);
			handGfx.localEulerAngles = (angle * Mathf.Rad2Deg - 90) * Vector3.forward;
			if (weaponJoint != null)
			{
				angle = -angle + (Mathf.PI / 2);
				float modulo = Mod(realAngularOffset, Mathf.PI * 2);
				//Debug.Log("RealAngularOffset before: " + realAngularOffset + "\tangle: " + angle + "\tmodulo: " + modulo);
				if (Mathf.Abs(modulo - angle) * Mathf.Rad2Deg > 1)
				{
					float angleDiff = Mod(angle - modulo, Mathf.PI * 2);
					if (angleDiff < Mathf.PI)
					{
						realAngularOffset += angleDiff;
						weaponJoint.angularOffset = realAngularOffset;
					}
					else if (angleDiff > Mathf.PI)
					{
						realAngularOffset -= (Mathf.PI * 2 - angleDiff);
						weaponJoint.angularOffset = realAngularOffset;
					}
					//Debug.Log("RealAngularOffset after: " + realAngularOffset + "\tangleDiff: " + angleDiff);
				}
			}
		}
		// TODO: Need to figure out how to handle collision/slashing mechanics
	}

	// Update is called once per frame
	private void Update()
	{

	}

	#region Input Callbacks
	public void Move(InputAction.CallbackContext ctx)
	{

	}

	public void Jump(InputAction.CallbackContext ctx)
	{

	}

	public void Down(InputAction.CallbackContext ctx)
	{

	}

	public void Wield(InputAction.CallbackContext ctx)
	{

	}
	#endregion

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.gameObject.layer | groundLayerMask.value) != 0)
			onGround++;
		if (onGround > 0)
			airJumpLeft = airJumpCount;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if ((collision.gameObject.layer | groundLayerMask.value) != 0)
			onGround--;
	}
}
