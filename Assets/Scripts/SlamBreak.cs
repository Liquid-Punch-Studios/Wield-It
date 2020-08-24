using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SlamBreak : MonoBehaviour
{
	public GameObject brokenDoor;
	public Transform explosionPoint;
	public AudioPlayer breakSound;
	public CinemachineImpulseSource impulse;

	public void Break()
	{
		Vector3 explosion = explosionPoint.position;

		gameObject.SetActive(false);
		brokenDoor.SetActive(true);
		foreach (Transform pieces in brokenDoor.transform)
			pieces.GetComponent<Rigidbody>().AddExplosionForce(1000, explosion, 100);

		breakSound?.PlayRandom(0.1f);
		impulse.GenerateImpulse();
	}
}
