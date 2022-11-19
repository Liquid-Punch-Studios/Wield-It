using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementActions
{
	Idle,
	Jump,
	DoubleJump,
	Dash,
	AirDash,
	Slam
}

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

	private Controls controls;

	/// <summary>
	/// How much the hand moves in relation to the mouse delta
	/// </summary>
	const float sensitivity = 0.02f;

    private MovementActions movementState;
    public MovementActions MovementState {
		get => movementState;
		set {
			lastMovementState = movementState;
			if(lastMovementState != value)
            {
				stateChanged = true;
				lastMovementStateTime = Time.time;
				movementState = value;
			}
			
		}
	}
    [HideInInspector]
	public MovementActions lastMovementState;
	private float lastMovementStateTime;

	private bool stateChanged;

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

		MovementState = MovementActions.Idle;
		lastMovementState = MovementActions.Idle;
	}

    private void OnEnable()
    {
		EnablePlayerController();
		if (movement == null) movement = GetComponent<Movement>();
		movement.OnGroundSet += Movement_OnGroundSet;
	}

    private void OnDisable()
    {
		movement.OnGroundSet -= Movement_OnGroundSet;
		DisablePlayerController();
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
		
		switch (MovementState)
        {
			case MovementActions.Idle:
				//to Jump
				if (controls.Player.Jump.triggered)
					MovementState = MovementActions.Jump;
				//to Dash
				else if (controls.Player.Dash.triggered)
					MovementState = MovementActions.Dash;
				break;

			case MovementActions.Jump:

				//do Jump
				if (stateChanged)
                {
					movement.Jump();
					movement.OnGround = false;
					stateChanged = false;
                }

				//to Idle (with Movement_OnGroundSet)
				//to DoubleJump
				if (controls.Player.Jump.triggered && Time.time - lastMovementStateTime > 0.05f)
					MovementState = MovementActions.DoubleJump;
				//to AirDash
				else if (controls.Player.Dash.triggered)
					MovementState = MovementActions.AirDash;
				//to Slam
				else if (controls.Player.Slam.triggered)
					MovementState = MovementActions.Slam;
				break;

			case MovementActions.DoubleJump:

				//do DoubleJump
				if (stateChanged)
				{
					movement.Jump();
					stateChanged = false;
				}

				//to Idle (with Movement_OnGroundSet)
				//to AirDash
				if (controls.Player.Dash.triggered)
					MovementState = MovementActions.AirDash;
				//to Slam
				else if (controls.Player.Slam.triggered)
					MovementState = MovementActions.Slam;
				break;

			case MovementActions.Dash:
				if (stateChanged)
				{
					var dir = controls.Player.Move.ReadValue<float>();
					stateChanged = false;
					if (movement.Dash(dir))
						ChangeDirection(dir);
				}
				//to Idle (with animation end)
				//to Jump
				if (controls.Player.Jump.triggered)
					MovementState = MovementActions.Jump;
				else if (controls.Player.Slam.triggered)
					MovementState = MovementActions.Slam;

				break;

			case MovementActions.AirDash:
				if (stateChanged && movement.airDashLeft > 0)
				{
					movement.airDashLeft--;
					var dir = controls.Player.Move.ReadValue<float>();
					stateChanged = false;
					if (movement.Dash(dir))
						ChangeDirection(dir);
				}
				//to Idle (with animation end)
				//to DoubleJump
				if (controls.Player.Jump.triggered)
					MovementState = MovementActions.DoubleJump;
				else if (controls.Player.Slam.triggered)
					MovementState = MovementActions.Slam;
				break;

			case MovementActions.Slam:
				//do Slam
				if (stateChanged)
				{
					movement.Slam();
					stateChanged = false;
				}

				//to Idle (and with animation end and Movement_OnGroundChanged)
				//to DoubleJump (if last jump)
				if (controls.Player.Jump.triggered && lastMovementState == MovementActions.Jump)
					MovementState = MovementActions.DoubleJump;
				break;
        }

		float horizontal = controls.Player.Move.ReadValue<float>();
		movement.move = horizontal;
		if (controls.Player.Move.triggered)
			ChangeDirection(horizontal);

		animator.SetFloat("speed X", Mathf.Abs(rb.velocity.x / moveSpeed));
		animator.SetFloat("speed Y", rb.velocity.y);

        //if (controls.Player.Dash.triggered)
        //{
        //    var dir = controls.Player.Move.ReadValue<float>();
        //    if (movement.Dash(dir))
        //        ChangeDirection(dir);
        //}

        //if (controls.Player.Slam.triggered)
        //    movement.Slam();
        //else if (controls.Player.Jump.triggered)
        //    movement.Jump();

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
		GameManager.Instance.controls.Player.Disable();
    }
	public void EnablePlayerController()
	{
		GameManager.Instance.controls.Player.Enable();
	}
	private void Movement_OnGroundSet(object sender, System.EventArgs e)
	{
		MovementState = MovementActions.Idle;
		movement.airDashLeft = movement.maxAirdash;
	}
}
