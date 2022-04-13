using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Handler : MonoBehaviour
{
	public Transform hand;

	public GameObject weapon;
	private Rigidbody weaponRb;
	public Rigidbody WeaponRb
    {
        get { return weaponRb; }
        set { weaponRb = value; }
    }

	private ConfigurableJoint weaponJoint;

	public int velocityCount = 10;
	CircularBuffer<Vector3> velocityList;
	public float throwSensitivity = 1f;
	public float throwSpeedLimit = 50f;
	public float thresholdMultiplier = 10f;

	private void Awake()
	{
		velocityList = new CircularBuffer<Vector3>(velocityCount);
	}

	private void Start()
	{
		playerRb = GetComponent<Rigidbody>();
		ChangeWeapon(weapon);
	}

	private void FixedUpdate()
	{
		Vector2 delta = GameManager.Instance.controls.Player.Wield.ReadValue<Vector2>();
		velocityList.Add(delta);
	}

	private Rigidbody playerRb;

	/// <summary>
	/// This is the point hand & weapon rotates against
	/// </summary>
	public Vector2 shoulderOffset;

	/// <summary>
	/// How much further weapon can be held from shoulder
	/// </summary>
	public float armLength = 1.5f;

	/// <summary>
	/// Controls whether the hand rotation is disabled
	/// </summary>
	public bool fixedWeaponAngle;
	// This controls whether weapon is held in a fixed angle
	// Usually used when stabbing enemies and terrain, also 
	// makes some cool air slam possible

	public float sensitivity = 0.02f;

	public void Wield(Vector2 delta)
	{
		if (delta != Vector2.zero)
		{
			Vector3 pos = hand.localPosition + (Vector3)delta;
			// Need to keep Z unchanged here as well
			if (((Vector2)pos - shoulderOffset).sqrMagnitude > armLength * armLength)
			{
				Vector2 reach = Vector2.ClampMagnitude((Vector2)pos, armLength);
				pos = hand.localPosition = new Vector3(reach.x, reach.y, pos.z);
			}
			else
				hand.localPosition = pos;

			if (!fixedWeaponAngle)
			{
				float angle = Mathf.Atan2(pos.y - shoulderOffset.y, pos.x - shoulderOffset.x);
				hand.localEulerAngles = (angle * Mathf.Rad2Deg) * Vector3.forward;
			}
		}
	}

	public void Throw(Vector2 velocity)
	{
		var throwableRemain = GameObject.Find("ThrowableRemaining").GetComponent<TextMeshProUGUI>();
		GameObject thrownWeaponPrefab = weapon.GetComponent<Sword>().thrownWeaponPrefab;
		
		if (thrownWeaponPrefab == null)
		{
			Debug.Log("Thrown weapon prefab is null.");
			return;
		}
		var radialWeapon = GameObject.Find("RadialMenu").GetComponent<RadialMenu>().menu[RadialMenu.segment];

		if (radialWeapon.Amount <= 0)
			return;

		Vector3 pos = weaponRb.position;
		Quaternion rot = weaponRb.rotation;

		//Vector3 vel = velocity;
		Vector3 vel = velocityList.Aggregate((s, v) => s + v) * throwSensitivity / velocityList.Count;
		vel = vel.normalized * Mathf.Min(vel.magnitude, throwSpeedLimit);

		if (vel.magnitude < thresholdMultiplier * throwSensitivity)
			return;

		vel += playerRb.velocity;

		//Debug.LogWarning($"Average velocity: {vel}");
		Vector3 ang = weaponRb.angularVelocity;

		var obj = Instantiate(thrownWeaponPrefab, pos, rot);
		radialWeapon.Amount--;

		throwableRemain.text = radialWeapon.Amount + "/" + radialWeapon.maxAmount;
		
		if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
		{
			rb.velocity = vel;
			rb.angularVelocity = ang;
		}
	}
	private static Transform tempChild = null;
	private static Transform tempParent = null;

	private static Vector3[] positionRegister;
	private static float[] posTimeRegister;
	private static int positionSamplesTaken = 0;

	private static Quaternion[] rotationRegister;
	private static float[] rotTimeRegister;
	private static int rotationSamplesTaken = 0;

	public static void Init()
	{
		tempChild = (new GameObject("Math3d_TempChild")).transform;
		tempParent = (new GameObject("Math3d_TempParent")).transform;

		tempChild.gameObject.hideFlags = HideFlags.HideAndDontSave;
		MonoBehaviour.DontDestroyOnLoad(tempChild.gameObject);

		tempParent.gameObject.hideFlags = HideFlags.HideAndDontSave;
		MonoBehaviour.DontDestroyOnLoad(tempParent.gameObject);

		//set the parent
		tempChild.parent = tempParent;
	}
	
	public static bool LinearAcceleration(out Vector3 vector, Vector3 position, int samples)
	{

		Vector3 averageSpeedChange = Vector3.zero;
		vector = Vector3.zero;
		Vector3 deltaDistance;
		float deltaTime;
		Vector3 speedA;
		Vector3 speedB;

		//Clamp sample amount. In order to calculate acceleration we need at least 2 changes
		//in speed, so we need at least 3 position samples.
		if (samples < 3)
		{

			samples = 3;
		}

		//Initialize
		if (positionRegister == null)
		{

			positionRegister = new Vector3[samples];
			posTimeRegister = new float[samples];
		}

		//Fill the position and time sample array and shift the location in the array to the left
		//each time a new sample is taken. This way index 0 will always hold the oldest sample and the
		//highest index will always hold the newest sample. 
		for (int i = 0; i < positionRegister.Length - 1; i++)
		{

			positionRegister[i] = positionRegister[i + 1];
			posTimeRegister[i] = posTimeRegister[i + 1];
		}
		positionRegister[positionRegister.Length - 1] = position;
		posTimeRegister[posTimeRegister.Length - 1] = Time.time;

		positionSamplesTaken++;

		if (positionSamplesTaken >= samples)
		{

			//Calculate average speed change.
			for (int i = 0; i < positionRegister.Length - 2; i++)
			{

				deltaDistance = positionRegister[i + 1] - positionRegister[i];
				deltaTime = posTimeRegister[i + 1] - posTimeRegister[i];

				//If deltaTime is 0, the output is invalid.
				if (deltaTime == 0)
				{

					return false;
				}

				speedA = deltaDistance / deltaTime;
				deltaDistance = positionRegister[i + 2] - positionRegister[i + 1];
				deltaTime = posTimeRegister[i + 2] - posTimeRegister[i + 1];

				if (deltaTime == 0)
				{

					return false;
				}

				speedB = deltaDistance / deltaTime;

				//This is the accumulated speed change at this stage, not the average yet.
				averageSpeedChange += speedB - speedA;
			}

			//Now this is the average speed change.
			averageSpeedChange /= positionRegister.Length - 2;

			//Get the total time difference.
			float deltaTimeTotal = posTimeRegister[posTimeRegister.Length - 1] - posTimeRegister[0];

			//Now calculate the acceleration, which is an average over the amount of samples taken.
			vector = averageSpeedChange / deltaTimeTotal;

			return true;
		}

		else
		{

			return false;
		}
	}

	public void ChangeWeapon(GameObject weapon)
	{
		weaponRb = weapon.GetComponent<Rigidbody>();
		weaponJoint = weapon.GetComponent<ConfigurableJoint>();
	}
}
