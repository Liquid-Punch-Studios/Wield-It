using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
	private GameManager game;
	private SettingsManager settings;

	private Animator animator;
	private Rigidbody rb;
	private float moveSpeed;

	private Health health;
	private Stamina stamina;
	private Handler handler;
	private Movement movement;

	//public bool throwable;

	public Transform playerGfx;
	public Transform handGfx;

	/// <summary>
	/// How much the hand moves in relation to the mouse delta
	/// </summary>
	private Controls controls;

	const float sensitivity = 0.02f;

	// Awake is called once during the lifetime of the script, on its initial awake state, prior to any other functions
	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Start is called before the first frame update
	private void Start()
	{
		controls = GameManager.Instance.controls;
		game = GameObject.FindObjectOfType<GameManager>();
		settings = GameObject.FindObjectOfType<SettingsManager>();

		health = GetComponent<Health>();
		stamina = GetComponent<Stamina>();
		handler = GetComponent<Handler>();
		movement = GetComponent<Movement>();

		rb = GetComponent<Rigidbody>();
		moveSpeed = GetComponent<Movement>().moveSpeed;
		animator = GetComponentInChildren<Animator>();
	}

	


	/// Helper function for math-like positive modulus
	public static float Mod(float x, float m)
	{
		var r = x % m;
		return r < 0 ? r + m : r;
	}

	public void ChangeDirection(float dir)
	{
		if ((dir = Mathf.Sign(dir)) != 0)
		{
			playerGfx.localScale = new Vector3(dir * Mathf.Abs(playerGfx.localScale.x), playerGfx.localScale.y, playerGfx.localScale.z);
			handGfx.localScale = new Vector3(handGfx.localScale.x, dir * Mathf.Abs(handGfx.localScale.y), handGfx.localScale.z);
			var weapon = handler.weapon.transform;
			weapon.localScale = new Vector3( weapon.localScale.x, dir * Mathf.Abs(weapon.localScale.y), weapon.localScale.z);
		}
	}

	// FixedUpdate is called once per physics frame
	private void FixedUpdate()
	{
		float horizontal = controls.Player.Move.ReadValue<float>();
		movement.move = horizontal;
		if (controls.Player.Move.triggered)
			ChangeDirection(horizontal);

		animator.SetFloat("speed X", Mathf.Abs(rb.velocity.x / moveSpeed));
		animator.SetFloat("speed Y", rb.velocity.y);

		if (controls.Player.Dash.triggered)
		{
			var dash = controls.Player.Dash.ReadValue<float>();
			if (movement.Dash(dash))
				ChangeDirection(dash);
		}

		if (controls.Player.Slam.triggered)
			movement.Slam();
		else if (controls.Player.Jump.triggered)
			movement.Jump();

		// Weapon movement delta
		Vector2 wield = controls.Player.Wield.ReadValue<Vector2>();
		handler.Wield(wield * sensitivity * SettingsManager.Instance.Settings.Sensitivity);

		
		handler.fixedWeaponAngle = controls.Player.Angle.ReadValue<float>() > InputSystem.settings.defaultButtonPressPoint;
		if (handler.weapon.GetComponent<Sword>().throwable)
			handler.fixedWeaponAngle = handler.weapon.name == "Spear(Clone)" ? handler.fixedWeaponAngle : false;

		if (controls.Player.ClickRelease.triggered)
		{
			if (handler.weapon.GetComponent<Sword>().throwable)
			{

				handler.Throw(Vector2.zero);
			}
		}
	}

	public void DisablePlayerController()
    {
		controls.Player.Disable();
    }
	public void EnablePlayerController()
	{
		controls.Player.Enable();
	}
}
