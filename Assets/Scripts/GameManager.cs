using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	public GameObject player;
	public GameObject weapon;
	public GameObject hand;

	public GameObject death;

	public Controls controls;

	public Transform respawnPoint;

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

	private void Awake()
	{
		controls = new Controls();
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

	private void Start()
	{
		playerHealth = player.GetComponent<Health>();
		playerStamina = player.GetComponent<Stamina>();
		playerMovement = player.GetComponent<Movement>();
		playerRb = player.GetComponent<Rigidbody>();
		spawnPoint = respawnPoint.position;

		weaponRb = weapon.GetComponent<Rigidbody>();
		weaponJoint = weapon.GetComponent<ConfigurableJoint>();
		weaponPosition = weapon.transform.position;
		weaponRotation = weapon.transform.rotation;

		handRb = hand.GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (playerHealth.Hp == 0)
		{
			if (player.activeSelf)
			{
				player.SetActive(false);
				var body = Instantiate(death);
				body.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);

				foreach (Rigidbody rb in body.GetComponentsInChildren<Rigidbody>())
				{
					rb.velocity = playerRb.velocity;
					rb.AddExplosionForce(200, Vector3.zero, 5, 1, ForceMode.Impulse);
				}

				//Time.timeScale = 0.25f;
				//Time.fixedDeltaTime *= 0.25f;
			}
			else if (controls.Player.Angle.triggered)
			{
				playerHealth.Hp = playerHealth.maxHp;
				playerStamina.sp = playerStamina.maxSp;
				playerRb.velocity = Vector3.zero;
				playerRb.angularVelocity = Vector3.zero;
				player.transform.localPosition = spawnPoint;
				playerMovement.ResetState();
				hand.transform.localPosition = Vector3.right;
				hand.transform.localRotation = Quaternion.identity;

				player.SetActive(true);

				weaponRb.velocity = Vector3.zero;
				weaponRb.angularVelocity = Vector3.zero;
				weapon.transform.localPosition = weaponPosition;
				weapon.transform.localRotation = weaponRotation;
				weaponJoint.connectedBody = handRb;

				//Time.timeScale = 1;
				//Time.fixedDeltaTime *= 4;
			}
		}
	}
}
