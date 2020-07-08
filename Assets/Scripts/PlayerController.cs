﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
	private Health health;
	private Stamina stamina;
	private Handler handler;
	private Movement movement;

	/// <summary>
	/// How much the hand moves in relation to the mouse delta
	/// </summary>
	public float sensitivity = 0.02f;

	// Awake is called once during the lifetime of the script, on its initial awake state, prior to any other functions
	private void Awake()
	{

	}

	// Start is called before the first frame update
	private void Start()
	{
		health = GetComponent<Health>();
		stamina = GetComponent<Stamina>();
		handler = GetComponent<Handler>();
		movement = GetComponent<Movement>();
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

	// FixedUpdate is called once per physics frame
	private void FixedUpdate()
	{
		float horizontal = controls.Player.Move.ReadValue<float>();
		movement.move = horizontal;

		if (controls.Player.Dash.triggered)
			movement.Dash(controls.Player.Dash.ReadValue<float>());

		if (controls.Player.Slam.triggered)
			movement.Slam();
		else if (controls.Player.Jump.triggered)
			movement.Jump();

		// Weapon movement delta
		Vector2 wield = controls.Player.Wield.ReadValue<Vector2>();
		handler.Wield(wield * sensitivity);

		handler.fixedWeaponAngle = controls.Player.Angle.ReadValue<float>() > InputSystem.settings.defaultButtonPressPoint;
	}

	// Update is called once per frame
	private void Update()
	{

	}
}