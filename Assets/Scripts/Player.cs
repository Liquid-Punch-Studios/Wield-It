using System.Collections;
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
	// FIXME: Mouse simulation for touch input is not implemented in the new input system yet

	public float moveSpeed = 4;
	public float moveForce = 100;
	public float jumpSpeed = 8;
	public float airMoveSpeed = 4;
	public float airMoveForce = 100;
	public float airJumpSpeed = 8;
	// TODO: Decide if control percent for air is better
	public int airJumpCount = 1;
	private int airJumpLeft;
	public float dashSpeed = 25;
	public float dashCooldown = 2;
	private float lastDashTime = 0;

	private int onGround = 0;
	public bool OnGround { get => onGround > 0; }

	// TODO: Not sure if we should restrict player to
	// only be able to jump on static ground, maybe
	// allow jumping from heads of enemies?
	public LayerMask groundLayerMask;

	private Rigidbody playerRb;

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
				weaponRb = weapon.GetComponent<Rigidbody>();
				weaponJoint = weapon.GetComponent<Joint>();
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
	private Rigidbody weaponRb;
	private Joint weaponJoint;

	// This controls whether weapon is held in a fixed angle
	// Usually used when stabbing enemies and terrain, also 
	// makes some cool air slam possible
	private bool fixedWeaponAngle;

	// Awake is called once during the lifetime of the script, on its initial awake state, prior to any other functions
	private void Awake()
	{

	}

	// Start is called before the first frame update
	private void Start()
	{
		playerRb = GetComponent<Rigidbody>();
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

		if (controls.Player.DashLeft.triggered ^ controls.Player.DashRight.triggered && 
			Time.time - lastDashTime > dashCooldown) // TODO: Check if overflow is a problem
		{
			float dash = controls.Player.DashLeft.triggered ? -1 : controls.Player.DashRight.triggered ? 1 : 0;
			playerRb.velocity = Vector3.right * dash * dashSpeed;
			lastDashTime = Time.time;
		}

		// Apply some force in the direction we want to
		// move if we're not already moving in that vector with enough speed
		// Bigger the difference bigger the force
		// FIXME: Needs fine tuning, experimental
		float speed = OnGround ? moveSpeed : airMoveSpeed;
		{
			float force = Mathf.Clamp(speed * move - playerRb.velocity.x, -speed, speed) / speed * moveForce;
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
			if (controls.Player.Angle.ReadValue<float>() == 0)
				// Maybe check < 0.1f or something?
			{
				float angle = Mathf.Atan2(pos.y - shoulderOffset.y, pos.x - shoulderOffset.x);
				hand.localEulerAngles = (angle * Mathf.Rad2Deg) * Vector3.forward;
			}
		}
		// TODO: Need to figure out how to handle collision/slashing mechanics
	}

	// Update is called once per frame
	private void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if ((1 << other.gameObject.layer & groundLayerMask.value) != 0)
			onGround++;
		if (onGround > 0)
			airJumpLeft = airJumpCount;
	}

	private void OnTriggerExit(Collider other)
	{
		if ((1 << other.gameObject.layer & groundLayerMask.value) != 0)
			onGround--;
	}
}
