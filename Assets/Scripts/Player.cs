using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class Player : MonoBehaviour
{
	public float maxHealth = 100;
	public float maxStamina = 100;

	public float health = 100;
	public float stamina = 100;

	public float healthRegen = 0;
	public float staminaRegen = 0.5f;

	public Vector2 shoulderOffset; /// This is the point hand & weapon rotates against
	public float armLength = 1.5f; /// How much further weapon can be held from shoulder
	public float wieldFactor = 0.02f; /// How much the hand moves in relation to the mouse/touch delta
	// TODO: Maybe seperate mouse & touch?
	// FIXME: Mouse simulation for touch input is not implemented in the new input system yet

	public float moveSpeed = 8;
	public float moveForce = 5000;
	
	public float jumpSpeed = 12;
	public float jumpCost = 10;
	
	public float airMoveSpeed = 4;
	public float airMoveForce = 5000;
	
	public float airJumpSpeed = 12; // TODO: Decide if control percent for air is better
	public float airJumpCost = 10;
	public int airJumpCount = 1;
	private int airJumpLeft;

	public float dashSpeed = 25;
	public int dashCost = 50;
	public float dashPressDuration = 0.2f;
	public float dashReleaseDuration = 0.2f;

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
				weaponJoint = weapon.GetComponent<ConfigurableJoint>();
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
	private ConfigurableJoint weaponJoint;

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

	private Controls controls;

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

	enum DashState
	{
		Out0,
		In1,
		Out1,
		In2,
	}

	private DashState dashState = DashState.Out0;
	private float dashLastInput;
	private float dashDirection = 0;

	// FixedUpdate is called once per physics frame
	private void FixedUpdate()
	{
		health = Mathf.Min(maxHealth, health + healthRegen);
		stamina = Mathf.Min(maxStamina, stamina + staminaRegen);

		float move = controls.Player.Move.ReadValue<float>();

		// Hand written state machine for detecting double tap input for dash
		bool dash = false;
		switch (dashState)
		{
			case DashState.Out0:
				if (Mathf.Abs(move) >= InputSystem.settings.defaultButtonPressPoint)
				{
					dashDirection = Mathf.Sign(move);
					dashState = DashState.In1;
					dashLastInput = Time.fixedTime;
				}
				break;
			case DashState.In1:
				if (Mathf.Abs(move) < InputSystem.settings.defaultButtonPressPoint)
				{
					if (Time.fixedTime - dashLastInput < dashPressDuration)
					{
						dashState = DashState.Out1;
						dashLastInput = Time.fixedTime;
					}
					else
						dashState = DashState.Out0;
				}
				else if (Mathf.Sign(move) != dashDirection)
				{
					dashDirection = Mathf.Sign(move);
					dashLastInput = Time.fixedTime;
				}
				break;
			case DashState.Out1:
				if (Mathf.Abs(move) >= InputSystem.settings.defaultButtonPressPoint &&
					Mathf.Sign(move) == dashDirection &&
					Time.fixedTime - dashLastInput < dashReleaseDuration)
				{
					dash = true;
					dashState = DashState.In2;
					dashLastInput = Time.fixedTime;
				}
				else if (Mathf.Abs(move) >= InputSystem.settings.defaultButtonPressPoint)
				{
					dashDirection = Mathf.Sign(move);
					dashState = DashState.In1;
					dashLastInput = Time.fixedTime;
				}
				break;
			case DashState.In2:
				if (Mathf.Abs(move) < InputSystem.settings.defaultButtonPressPoint)
				{
					dashState = DashState.Out0;
				}
				break;
		}
		
		if (dash && stamina >= dashCost)
		{
			playerRb.velocity = Vector3.right * dashDirection * dashSpeed;
			stamina -= dashCost;
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
			if (OnGround && stamina >= jumpCost)
			{
				vel.y = jumpSpeed;
				playerRb.velocity = vel;
				stamina -= jumpCost;
			}
			else if (airJumpLeft > 0 && stamina >= airJumpCost)
			{
				vel.y = airJumpSpeed;
				playerRb.velocity = vel;
				airJumpLeft--;
				stamina -= airJumpCost;
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
