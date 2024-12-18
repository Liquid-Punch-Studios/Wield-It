﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
	public GameSettings settings;

	public GameObject player;
	public GameObject weapon;
	public GameObject hand;

	public GameObject deadPlayerPrefab;

	public Controls controls;

	private Transform respawnPoint;
	public Transform RespawnPoint
    {
		get
        {
			return respawnPoint;
        }
        set
        {
			respawnPoint = value;
			spawnPoint = respawnPoint.position;
        }
    }

	private Health playerHealth;
	private Stamina playerStamina;
	private Movement playerMovement;
	private Rigidbody playerRb;
	private Vector3 spawnPoint;

	private Rigidbody weaponRb;
	private ConfigurableJoint weaponJoint;
	private Vector3 weaponPosition;
	private Quaternion weaponRotation;

	private Rigidbody handRb;

	protected override void Awake()
	{
		base.Awake();
		controls = new Controls();
	}

	private void OnEnable()
	{
		if (controls == null)
			controls = new Controls();
		controls.Enable();

		SceneManager.sceneLoaded		+= SceneManager_sceneLoaded;
		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
	}

	private void OnDisable()
	{
		SceneManager.activeSceneChanged	-= SceneManager_activeSceneChanged;
		SceneManager.sceneLoaded		-= SceneManager_sceneLoaded;
		
		controls.Disable();
	}

	private void SceneManager_activeSceneChanged(Scene current, Scene next)
	{
		Debug.Log($"Scene Changed: {current.name} => {next.name}");

		player = null;
		weapon = null;

		playerHealth = null;

		playerStamina = null;
		playerMovement = null;
		playerRb = null;
		spawnPoint = Vector3.zero;

		weaponRb = null;
		weaponJoint = null;
		weaponPosition = Vector3.zero;
		weaponRotation = Quaternion.identity;

		handRb = null;
	}

	private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
	{
		Debug.Log($"Scene Loaded: {scene.name}\n    handle: {scene.handle}\n    index: {scene.buildIndex}\n    path: {scene.path}");

		player = GameObject.FindGameObjectWithTag("Player");
		weapon = GameObject.FindGameObjectWithTag("MainWeapon");
		playerHealth = player.GetComponent<Health>();
		
		playerStamina = player.GetComponent<Stamina>();
		playerMovement = player.GetComponent<Movement>();
		playerRb = player.GetComponent<Rigidbody>();
		spawnPoint = transform.position;

		if (weapon != null)
        {
			weaponRb = weapon.GetComponent<Rigidbody>();
			weaponJoint = weapon.GetComponent<ConfigurableJoint>();
			weaponPosition = weapon.transform.position;
			weaponRotation = weapon.transform.rotation;
		}
		

		handRb = hand.GetComponent<Rigidbody>();
	}

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (playerHealth != null && playerHealth.Hp == 0)
		{
			if (player.activeSelf)
			{
				player.SetActive(false);
				var body = Instantiate(deadPlayerPrefab);
				body.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);

				foreach (Rigidbody rb in body.GetComponentsInChildren<Rigidbody>())
				{
					rb.velocity = playerRb.velocity;
					rb.AddExplosionForce(200, Vector3.zero, 5, 1, ForceMode.Impulse);
				}

				//Time.timeScale = 0.25f;
				//Time.fixedDeltaTime *= 0.25f;
			}
			else if (controls.UI.MouseClick.triggered)
			{
				playerHealth.Hp = playerHealth.MaxHp;
				playerStamina.Sp = playerStamina.MaxSp;
				playerRb.velocity = Vector3.zero;
				playerRb.angularVelocity = Vector3.zero;
				player.transform.localPosition = spawnPoint;
				playerMovement.ResetState();
				hand.transform.localPosition = Vector3.right;
				hand.transform.localRotation = Quaternion.identity;

				player.SetActive(true);

				if (weapon != null)
                {
					weaponRb.velocity = Vector3.zero;
					weaponRb.angularVelocity = Vector3.zero;
					weapon.transform.localPosition = weaponPosition;
					weapon.transform.localRotation = weaponRotation;
					weaponJoint.connectedBody = handRb;
				}

				//Time.timeScale = 1;
				//Time.fixedDeltaTime *= 4;
			}
		}
	}
}
